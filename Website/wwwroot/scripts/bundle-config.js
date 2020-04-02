({
    baseUrl: "../lib/",
    //mainConfigFile: "references.js",
    paths: {
        // JQuery:
        "jquery": "jquery/dist/jquery.min",
        //"jquery-ui/ui/widget": "jquery-ui/ui/widget",
        "jquery-ui-all": "jquery-ui/jquery-ui",
        //"jquery-ui/ui/focusable": "jquery-ui/ui/focusable",
        "jquery-validate": "jquery-validation/dist/jquery.validate",
        "jquery-validate-unobtrusive": "jquery-validation-unobtrusive/src/jquery.validate.unobtrusive",

        // Jquery plugins:
        "alertify": "alertifyjs/dist/js/alertify",
        "smartmenus": "smartmenus/dist/jquery.smartmenus.min",
        "file-upload": "jquery-file-upload/js/jquery.fileupload",
        "jquery-typeahead": "jquery-typeahead/dist/jquery.typeahead.min",
        "combodate": "combodate/src/combodate",
        "js-cookie": "js-cookie/src/js.cookie",
        "handlebars": "handlebars/handlebars.amd.min", //it can be something else
        "hammerjs": "hammer.js/hammer.min",
        "jquery-mentions": "jquery-mentions-input/jquery.mentionsInput",
        "chosen": "chosen-js/chosen.jquery.min",

        // Bootstrap
        "popper": "popper.js/dist/umd/popper.min",
        "bootstrap": "bootstrap/dist/js/bootstrap.min",
        "validation-style": "jquery-validation-bootstrap-tooltip/jquery-validate.bootstrap-tooltip.min",
        "file-style": "bootstrap-filestyle/src/bootstrap-filestyle.min",
        "spinedit": "bootstrap-spinedit/js/bootstrap-spinedit",
        "password-strength": "pwstrength-bootstrap/dist/pwstrength-bootstrap-1.2.7.min",
        "slider": "seiyria-bootstrap-slider/dist/bootstrap-slider.min",
        "moment": "moment/min/moment.min",
        "moment-locale": "moment/locale/en-gb",
        "datepicker": "eonasdan-bootstrap-datetimepicker/src/js/bootstrap-datetimepicker",
        "bootstrapToggle": "bootstrap-toggle/js/bootstrap-toggle.min",
        "bootstrap-select": "bootstrap-select/dist/js/bootstrap-select.min",
        "flickity": "flickity/dist/flickity.pkgd.min",
        'olive': "olive.mvc/dist"
    },
    map: {
        "*": {
            "popper.js": "popper",
            '../moment': 'moment',
            "app": "../scripts",
            "jquery-sortable": "jquery-ui/ui/widgets/sortable"
            //"jquery-ui/ui/widget": "jquery-ui/ui/widget",
            //"jquery-ui/ui/version": "jquery-ui/ui/version",
            //"jquery-ui/ui/widget": "jquery-ui/ui/widget",
            //"../ie": "jquery-ui/ui/ie",
            //"../version": "jquery-ui/ui/version",
            //"../widget": "jquery-ui/ui/widget",
            //"../data": "jquery-ui/ui/data",
            //"../scroll-parent": "jquery-ui/ui/scroll-parent",
            //"mouse": "jquery-ui/ui/widgets/mouse",
        }
    },
    shim: {
        "jquery-ui-all": ["jquery"],
        "bootstrap": ["jquery", "popper"],
        "bootstrap-select": ['jquery', 'bootstrap'],
        "bootstrapToggle": ["jquery"],
        "jquery-validate": ['jquery'],
        "validation-style": ['jquery', "jquery-validate", "bootstrap"],
        "combodate": ['jquery'],
        "jquery-typeahead": ['jquery'],
        "file-upload": ['jquery', 'jquery-ui-all'],
        "file-style": ["file-upload"],
        "smartmenus": ['jquery'],
        "chosen": ['jquery'],
        "jquery-validate-unobtrusive": ["jquery", "jquery-validate"],
        'backbone.layoutmanager': ['backbone'],
        "spinedit": ['jquery'],
        "password-strength": ['jquery'],
        "moment-locale": ['moment'],
        "olive/extensions/jQueryExtensions": {
            deps: ['jquery', "jquery-validate-unobtrusive"],
            exports: '_'
        },
        "olive/olivePage": ["alertify", "olive/extensions/jQueryExtensions", "olive/extensions/systemExtensions", "combodate"],

        "app/appPage": ["jquery", "olive/olivePage"],

        "app/model/service": ["app/appPage", "olive/extensions/systemExtensions"],
        "app/featuresMenu/featuresMenu": ["app/model/service"],
        "app/featuresMenu/breadcrumbMenu": ["app/featuresMenu/featuresMenu"],
        "app/hub": ["app/featuresMenu/breadcrumbMenu"],
        "jquery-mentions": ['jquery', "underscore/underscore-min", "jquery-mentions-input/lib/jquery.elastic", "jquery-mentions-input/lib/jquery.events.input"]
    },
    optimize: "none",
    //generateSourceMaps: false,
    //preserveLicenseComments: false,    
    name: "../scripts/appPage",
    out: "bundle-built.js"
})