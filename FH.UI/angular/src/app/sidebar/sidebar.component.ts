import { Component, OnInit } from '@angular/core';
import { environment } from 'environments/environment';
import { Router } from '@angular/router';


export class RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
    childItems: RouteInfo[];
    onclick: any;
}

export var ROUTES: RouteInfo[] = [];

@Component({
    moduleId: module.id,
    selector: 'sidebar-cmp',
    templateUrl: 'sidebar.component.html',
    styleUrls: ['./sidebar.component.scss']
})

export class SidebarComponent implements OnInit {
    locationId = localStorage.getItem('MyLocationId');
    // currentOrderId = localStorage.getItem('CurrentOrderId');
    currentOrderId = '0';

    ROUTES_WELCOME: RouteInfo[] = [
        { path: '/welcome/login', title: 'Login', icon: 'nc-key-25', class: '', onclick: {}, childItems: [] },
        { path: '/welcome/register', title: 'Register', icon: 'nc-simple-add', onclick: {}, class: '', childItems: [] },
        { path: '/welcome/about-us', title: 'About Us', icon: 'nc-briefcase-24', onclick: {}, class: '', childItems: [] },
        { path: '/welcome/contact-us', title: 'Contact Us', icon: 'nc-send', onclick: {}, class: '', childItems: [] }
    ];

    ROUTES_MANAGER: RouteInfo[] = [
        { path: '/dashboard-manager/dashboard', title: 'Dashboard', icon: 'nc-layout-11', onclick: {}, class: '', childItems: [] },
        {
            path: '/dashboard-manager/location', title: 'Location', icon: 'nc-bank', onclick: {}, class: '', childItems: [
                { path: '/dashboard-manager/location', title: 'Management', icon: 'M', class: '', onclick: {}, childItems: [] },
                { path: '/dashboard-manager/location/' + this.locationId, title: 'Page', icon: 'P', class: '', onclick: {}, childItems: [] }]
        },
        { path: '/dashboard-manager/menu/0', title: 'Menu', icon: 'nc-book-bookmark', onclick: {}, class: '', childItems: [] },
        { path: '/dashboard-manager/table/0', title: 'Tables', icon: 'nc-caps-small', onclick: {}, class: '', childItems: [] },
        { path: '/dashboard-manager/table/my', title: 'My booking', icon: 'nc-bold', onclick: {}, class: '', childItems: [] },
        { path: '/dashboard-user/search', title: 'Search', icon: 'nc-zoom-split', onclick: {}, class: '', childItems: [] },
        // { path: '/dashboard-user/order/' + this.currentOrderId, title: 'Current Order', icon: 'nc-paper', onclick: {}, class: '', childItems: [] },
    ];

    ROUTES_USER: RouteInfo[] = [
        { path: '/dashboard-user/dashboard', title: 'Dashboard', icon: 'nc-layout-11', onclick: {}, class: '', childItems: [] },
        { path: '/dashboard-user/order/0', title: 'Current Order', icon: 'nc-paper', onclick: {}, class: '', childItems: [] },
        { path: '/dashboard-user/search', title: 'Search', icon: 'nc-zoom-split', onclick: {}, class: '', childItems: [] },
        { path: '/dashboard-user/table/my', title: 'My booking', icon: 'nc-bold', onclick: {}, class: '', childItems: [] },
    ];

    constructor(private router: Router) { }

    isLogin = (localStorage.getItem('token') != null);
    isOrder = (localStorage.getItem('CurrentOrderId') != null);
    isManager = ((this.isLogin) && (localStorage.getItem('IsManager').toLocaleLowerCase() == 'true'));
    isCurrentUser = ((this.isLogin) && (localStorage.getItem('CurrentRole').toLocaleLowerCase() == 'false'));
    fullName = localStorage.getItem('FullName');
    icon = environment.serverURL + localStorage.getItem('Icon');
    menuItems: any[];
    currentRole = 'Manager';

    public userItems: RouteInfo[] = [
        {
            path: '#', title: this.fullName, icon: this.icon, class: '', onclick: {}, childItems: [
                { path: '#', title: 'Switch current role to ' + this.currentRole, icon: 'S', class: 'switch-role-btn', onclick: this.switchRole, childItems: [] },
                { path: '#', title: 'Logout', icon: 'X', class: 'logout-btn', onclick: this.logout, childItems: [] }]
        }

    ];

    ngOnInit() {

        this.isLogin = (localStorage.getItem('token') != null);
        this.isManager = ((this.isLogin) && (localStorage.getItem('IsManager').toLocaleLowerCase() == 'true'));
        this.isCurrentUser = ((this.isLogin) && (localStorage.getItem('CurrentRole').toLocaleLowerCase() == 'false'));
        this.fullName = localStorage.getItem('FullName');
        this.icon = environment.serverURL + localStorage.getItem('Icon');

        ROUTES = this.ROUTES_WELCOME.filter(menuItem => menuItem);
        if (this.isLogin) {
            (!this.isCurrentUser ? ROUTES = this.ROUTES_MANAGER.filter(menuItem => menuItem) : ROUTES = this.ROUTES_USER.filter(menuItem => menuItem));
            ROUTES.concat(this.userItems.filter(menuItem => menuItem));
        }
        this.menuItems = ROUTES.filter(menuItem => menuItem);
    }

    ngAfterViewInit() {
        const dropdowns = document.getElementsByClassName("dropdown-item");
        let i: number;
        for (i = 0; i < dropdowns.length; i++) {
            dropdowns[i].removeEventListener("click", function () {
                this.classList.toggle("open");
                const dropdownContent = this.nextElementSibling;
                dropdownContent.classList.toggle("collapsed");
            });
            dropdowns[i].addEventListener("click", function () {
                this.classList.toggle("open");
                const dropdownContent = this.nextElementSibling;
                dropdownContent.classList.toggle("collapsed");
            });
        }

        //if (this.isLogin) {
        // const logout = document.getElementsByClassName("logout-btn");
        // logout[0].addEventListener("click", function () {
        //     localStorage.clear();
        //     window.location.reload(true);
        // });
        // const switchRole = document.getElementsByClassName("switch-role-btn");
        // switchRole[0].addEventListener("click", function () {
        //     if (this.isCurrentUser) {
        //         localStorage.setItem('CurrentRole', 'false');
        //         this.currentRole = "Manager";
        //         this.isCurrentUser = !this.isCurrentUser;
        //         document.location.reload(true)
        //         //this.router.navigateByUrl('/dashboard-user/dashboard')
        //     }
        //     else {
        //         localStorage.setItem('CurrentRole', 'true');
        //         this.currentRole = "Client";
        //         this.isCurrentUser = !this.isCurrentUser;
        //         document.location.reload(true)
        //         //this.router.navigateByUrl('/dashboard-manager/dashboard')
        //     }
        // });
        // }
    }

    public logout(e: any) {
        localStorage.clear();
        window.location.reload(true);
        this.router.navigateByUrl('/welcome/login');
    }


    public switchRole(e: any) {
        if (this.isCurrentUser) {
            localStorage.setItem('CurrentRole', 'false');
            this.currentRole = "Manager";
            this.isCurrentUser = !this.isCurrentUser;
            this.router.navigateByUrl('/dashboard-user/dashboard');
        }
        else {
            localStorage.setItem('CurrentRole', 'true');
            this.currentRole = "Client";
            this.isCurrentUser = !this.isCurrentUser;
            this.router.navigateByUrl('/dashboard-manager/dashboard');
        }
    }
}
