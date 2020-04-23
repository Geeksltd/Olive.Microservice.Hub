import OlivePage from 'olive/olivePage';
import { FeaturesMenuFactory } from 'app/featuresMenu/featuresMenu';
import AppContent from 'app/appContent';
import BadgeNumber from 'app/badgeNumber';
import ToggleCheckbox from 'app/toggleCheckbox';
import WidgetModule from 'app/widgetModule';
import ExpandCollapse from 'app/expandCollapse';
import BreadcrumbMenu from 'app/featuresMenu/breadcrumbMenu';
import { ServiceContainer } from 'olive/di/serviceContainer';
import Services from 'olive/di/services';
import HubAjaxRedirect from './overrides/hubAjaxRedirect';
import Url from 'olive/components/url';
import ResponseProcessor from 'olive/mvc/responseProcessor';
import Waiting from 'olive/components/waiting';
import HubForm from './overrides/hubForm';
import Validate from 'olive/components/validate';
import AjaxRedirect from 'olive/mvc/ajaxRedirect';
import HubServices from './hubServices';
import Hub from './hub';
import HubUrl from './overrides/hubUrl';
import HubModal from './hubModal';

//loading all modules
import 'jquery';
import 'jquery-ui-all';
import 'jquery-validate';
import 'jquery-validate-unobtrusive';
import 'alertify';
import 'smartmenus';
import 'file-upload';
import 'jquery-typeahead';
import 'combodate';
import 'js-cookie';
import 'handlebars';
import 'hammerjs';
import 'jquery-mentions';
import 'chosen';
import 'popper';
import 'bootstrap';
import 'validation-style';
import 'file-style';
import 'spinedit';
import 'password-strength';
import 'slider';
import 'moment';
import 'moment-locale';
import 'datepicker';
import 'bootstrapToggle';
import 'bootstrap-select';
import 'flickity';
import 'underscore';

export default class AppPage extends OlivePage {

    // Here you can override any of the base standard functions.
    // e.g: To use a different AutoComplete library, simply override handleAutoComplete(input).

    constructor() {
        super();
        this.getService<Hub>(HubServices.Hub).initialize();
        BadgeNumber.enableBadgeNumber($("a[data-badgeurl]"));

        //every 5 min badge numbers should be updated
        window.setInterval(() => { BadgeNumber.enableBadgeNumber($("a[data-badgeurl]")); }, 5 * 60 * 1000);

        ExpandCollapse.enableExpandCollapse($("#sidebarCollapse"));
        ExpandCollapse.enableExpandCollapse($("#taskBarCollapse"));

        $("#iFrameHolder").hide();
        $("iframe.view-frame").attr("src", "").attr("style", "");
    }

    configureServices(services: ServiceContainer) {

        services.addSingleton(Services.Url, () => new HubUrl());

        services.addSingleton(HubServices.Hub, (url: Url, ajaxRedirect: AjaxRedirect, featuresMenuFactory: FeaturesMenuFactory, breadcrumbMenu: BreadcrumbMenu) =>
            new Hub(url, ajaxRedirect, featuresMenuFactory, breadcrumbMenu))
            .withDependencies(Services.Url, Services.AjaxRedirect, HubServices.FeaturesMenuFactory, HubServices.BreadcrumbMenu);

        services.addSingleton(HubServices.FeaturesMenuFactory, (url: Url, waiting: Waiting, ajaxRedirect: AjaxRedirect) =>
            new FeaturesMenuFactory(url, waiting, ajaxRedirect))
            .withDependencies(Services.Url, Services.Waiting, Services.AjaxRedirect);

        services.addSingleton(HubServices.AppContent, (waiting: Waiting, ajaxRedirect: AjaxRedirect) =>
            new AppContent(waiting, ajaxRedirect))
            .withDependencies(Services.Waiting, Services.AjaxRedirect);

        services.addSingleton(HubServices.BreadcrumbMenu, (ajaxRedirect: AjaxRedirect) => new BreadcrumbMenu(ajaxRedirect))
            .withDependencies(Services.AjaxRedirect);

        services.addSingleton(Services.AjaxRedirect, (url: Url, responseProcessor: ResponseProcessor, waiting: Waiting) =>
            new HubAjaxRedirect(url, responseProcessor, waiting))
            .withDependencies(Services.Url, Services.ResponseProcessor, Services.Waiting);

        services.addSingleton(Services.Form, (url: Url, validate: Validate, waiting: Waiting, ajaxRedirect: AjaxRedirect) =>
            new HubForm(url, validate, waiting, ajaxRedirect))
            .withDependencies(Services.Url, Services.Validate, Services.Waiting, Services.AjaxRedirect);

        services.addSingleton(Services.ModalHelper, (url: Url, ajaxRedirect: AjaxRedirect, responseProcessor: ResponseProcessor) =>
            new HubModal(url, ajaxRedirect, responseProcessor))
            .withDependencies(Services.Url, Services.AjaxRedirect, Services.ResponseProcessor);

        super.configureServices(services);
    }

    revive() {
        super.initialize();
    }

    initialize() {

        super.initialize();
        this.getService<FeaturesMenuFactory>(HubServices.FeaturesMenuFactory).bindItemListClick();
        this.getService<BreadcrumbMenu>(HubServices.BreadcrumbMenu).bindItemListClick();
        const appcontext = this.getService<AppContent>(HubServices.AppContent);
        appcontext.enableContentBlock($("AppContent"));
        appcontext.enableHelp($("Help"));
        ToggleCheckbox.enableToggleCheckbox($("input[class='form-check']"));
        WidgetModule.enableWidget($("Widget"));

        let currentService = $("service[of]").attr("of");

        if (currentService) {
            currentService = currentService.toLocaleLowerCase();
        }

        // This function is called upon every Ajax update as well as the initial page load.
        // Any custom initiation goes here.
    }

    //loadFakeModules() {
    //    console.log(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, moment, x14, x15, x16, x17, x18, x19, x20, x21, x22, x23, x24);
    //}
}

window["page"] = new AppPage();