({
    baseUrl: "./",
    paths: {
        // JQuery:
        "jquery": "../lib/jquery/dist/jquery.min",
        "jquery-ui/ui/widget": "../lib/jquery-ui/ui/widget",
        "jquery-ui/ui/version": "../lib/jquery-ui/ui/version",
        "jquery-ui/ui/focusable": "../lib/jquery-ui/ui/focusable",
        "jquery-validate": "../lib/jquery-validation/dist/jquery.validate",
        "jquery-validate-unobtrusive": "../lib/jquery-validation-unobtrusive/src/jquery.validate.unobtrusive",

        // Jquery plugins:
        "alertify": "../lib/alertifyjs/dist/js/alertify",
        "smartmenus": "../lib/smartmenus/dist/jquery.smartmenus.min",
        "file-upload": "../lib/jquery-file-upload/js/jquery.fileupload",
        "jquery-typeahead": "../lib/jquery-typeahead/dist/jquery.typeahead.min",
        "combodate": "../lib/combodate/src/combodate",
        "js-cookie": "../lib/js-cookie/src/js.cookie",
        "handlebars": "../lib/handlebars/handlebars.amd.min", //it can be something else
        "hammerjs": "../lib/hammer.js/hammer.min",
        "jquery-mentions": "../lib/jquery-mentions-input/jquery.mentionsInput",
        "chosen": "../lib/chosen-js/chosen.jquery.min",

        // Bootstrap
        "popper": "../lib/popper.js/dist/umd/popper.min",
        "bootstrap": "../lib/bootstrap/dist/js/bootstrap.min",
        "validation-style": "../lib/jquery-validation-bootstrap-tooltip/jquery-validate.bootstrap-tooltip.min",
        "file-style": "../lib/bootstrap-filestyle/src/bootstrap-filestyle.min",
        "spinedit": "../lib/bootstrap-spinedit/js/bootstrap-spinedit",
        "password-strength": "../lib/pwstrength-bootstrap/dist/pwstrength-bootstrap-1.2.7.min",
        "slider": "../lib/seiyria-bootstrap-slider/dist/bootstrap-slider.min",
        "moment": "../lib/moment/min/moment.min",
        "moment-locale": "../lib/moment/locale/en-gb",
        "datepicker": "../lib/eonasdan-bootstrap-datetimepicker/src/js/bootstrap-datetimepicker",
        "bootstrapToggle": "../lib/bootstrap-toggle/js/bootstrap-toggle.min",
        "bootstrap-select": "../lib/bootstrap-select/dist/js/bootstrap-select.min",
        "flickity": "../lib/flickity/dist/flickity.pkgd.min",
        'olive': "../lib/olive.mvc/dist"
    },
    map: {
        "*": {
            "popper.js": "popper",
            '../moment': 'moment',
            "app": "../scripts",
            "jquery-sortable": "../lib/jquery-ui/ui/widgets/sortable"
        }
    },
    shim: {
        "bootstrap": ["jquery", "popper"],
        "bootstrap-select": ['jquery', 'bootstrap'],
        "bootstrapToggle": ["jquery"],
        "jquery-validate": ['jquery'],
        "validation-style": ['jquery', "jquery-validate", "bootstrap"],
        "combodate": ['jquery'],
        "jquery-typeahead": ['jquery'],
        "file-upload": ['jquery', 'jquery-ui/ui/widget'],
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
    name: "references",
    out: "bundle-built.js"
})