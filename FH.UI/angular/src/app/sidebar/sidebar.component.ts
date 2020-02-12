import { Component, OnInit } from '@angular/core';
import { environment } from 'environments/environment';
import { Router } from '@angular/router';


export class RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
    childItems: RouteInfo[];
}

export var ROUTES: RouteInfo[] = [];

const ROUTES_WELCOME: RouteInfo[] = [
    { path: '/welcome/login', title: 'Login', icon: 'nc-key-25', class: '', childItems: [] },
    { path: '/welcome/register', title: 'Register', icon: 'nc-simple-add', class: '', childItems: [] },
    { path: '/welcome/about-us', title: 'About Us', icon: 'nc-briefcase-24', class: '', childItems: [] },
    { path: '/welcome/contact-us', title: 'Contact Us', icon: 'nc-send', class: '', childItems: [] }
];

const ROUTES_MANAGER: RouteInfo[] = [
    { path: '/welcome/login', title: 'Login1', icon: 'nc-key-25', class: '', childItems: [] },
    { path: '/welcome/about-us', title: 'About Us1', icon: 'nc-briefcase-24', class: '', childItems: [] },
    { path: '/welcome/contact-us', title: 'Contact Us1', icon: 'nc-send', class: '', childItems: [] }
];

const ROUTES_USER: RouteInfo[] = [
    {
        path: '/welcome/login', title: 'Login2', icon: 'nc-key-25', class: '', childItems: [
            { path: '/welcome/register', title: 'Register', icon: 'nc-simple-add', class: '', childItems: [] }]
    },
    { path: '/welcome/register', title: 'Register2', icon: 'nc-simple-add', class: '', childItems: [] },
    { path: '/welcome/about-us', title: 'About Us2', icon: 'nc-briefcase-24', class: '', childItems: [] },
    { path: '/welcome/contact-us', title: 'Contact Us2', icon: 'nc-send', class: '', childItems: [] }
];

@Component({
    moduleId: module.id,
    selector: 'sidebar-cmp',
    templateUrl: 'sidebar.component.html',
    styleUrls: ['./sidebar.component.scss']
})

export class SidebarComponent implements OnInit {

    constructor(private router: Router) { }

    public isLogin = (localStorage.getItem('token') != null);
    public fullName = localStorage.getItem('FullName');
    public icon = environment.serverURL + localStorage.getItem('Icon');
    public menuItems: any[];
    public userItems: RouteInfo[] = [
        {
            path: '#', title: this.fullName, icon: this.icon, class: '', childItems: [
                { path: '#', title: 'Logout', icon: 'X', class: 'logout-btn', childItems: [] }]
        }

    ];

    ngOnInit() {
        ROUTES = ROUTES_WELCOME.filter(menuItem => menuItem);
        if (this.isLogin) {
            (localStorage.getItem('CurrentRole')) ? ROUTES = ROUTES_MANAGER.filter(menuItem => menuItem) : ROUTES = ROUTES_USER.filter(menuItem => menuItem);
            ROUTES.concat(this.userItems.filter(menuItem => menuItem));
        }
        this.menuItems = ROUTES.filter(menuItem => menuItem);
    }

    ngAfterViewInit() {
        const dropdowns = document.getElementsByClassName("dropdown-item");
        let i: number;
        for (i = 0; i < dropdowns.length; i++) {
            dropdowns[i].addEventListener("click", function () {
                this.classList.toggle("open");
                const dropdownContent = this.nextElementSibling;
                dropdownContent.classList.toggle("collapsed");
            });
        }

        const logout = document.getElementsByClassName("logout-btn");
        logout[0].addEventListener("click", function () {
            //this.logoutFunc()

            localStorage.clear();
            window.location.reload(true);
        });
    }

    // ngOnChanges() {
    //     this.isLogin = (localStorage.getItem('token') != null);
    //     this.fullName = localStorage.getItem('FullName');
    //     this.icon = environment.serverURL + localStorage.getItem('Icon');
    // }


}
