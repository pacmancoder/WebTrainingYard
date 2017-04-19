<?php
    // Simple fast template manager
    class PageComposer {

        // Requires path to template file for the current composer
        function __construct($templatePath) {
            $this->template = $templatePath;
            $this->variables = array();
            $this->chain = null;
        }

        // Can handle any displayable variable or another PageComposer
        function compose($varName, $varValue) {
            $this->variables[$varName] = $varValue;
            return $this;
        }

        // returns rendered document (string)
        function render() {
            if ($this->template != null) {
                // substitute template variables using "variable variables"
                foreach ($this->variables as $key => $var) {
                    if ($var instanceof PageComposer) {
                        ${$key} = $var->render();
                    } else {
                        ${$key} = $var;
                    }
                }
                // bufferize output and return as a string
                ob_start();
                    require $this->template;
                $out = ob_get_clean();
            } else {
                $out = '';
            }
            if ($this->chain != null) {
                return $out . $this->chain->render();
            } else {
                return $out;
            }
        }

        function chain($composer) {
            $this->chain = $composer;
            return $this->chain;
        }

        // private fields
        private $variables;
        private $template;
        private $chain;
    }
?>
