<?php
    // Simple fast template manager
    class PageComposer {

        // Requires path to template file for the current composer
        function __construct($templatePath) {
            $this->template = $templatePath;
            $this->variables = array();
        }

        // Can handle any displayable variable or another PageComposer
        function compose($varName, $varValue) {
            $this->variables[$varName] = $varValue;
        }

        // returns rendered document (string)
        function render() {
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
            return ob_get_clean();
        }

        // private fields
        private $variables;
        private $template;
    }
?>
