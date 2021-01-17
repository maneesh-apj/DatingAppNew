import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = { };
  constructor() { }
  //constructor(public authService: AuthService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
  }

  login() {
    console.log(this.model);
    //  this.authService.login(this.model).subscribe(next => {
    //     this.alertify.success('login successfull');
    //    }, error => {
    //      this.alertify.error(error);
    //    }, () => {
    //      this.router.navigate(['/members']);
    //    });
  }

  loggedIn() {
   // const token = localStorage.getItem('token');
    // return !!token;
    //return this.authService.loggedIn();
    return true;
  }

  logout() {
   //  localStorage.removeItem('token');
    // this.alertify.message('logged out');
    // this.router.navigate(['/home']);
  }

}
