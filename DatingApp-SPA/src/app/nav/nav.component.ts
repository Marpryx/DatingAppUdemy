import { AuthService } from './../_services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  login(){
    //console.log(this.model);
    this.authService.login(this.model).subscribe(next => {
      console.log('Logged in succsessfully');
    }, error => {
      console.log('Failed to login');
    }); //because it return Observable we need to subscrube to the method
  }

  loggedIn(){
    const token = localStorage.getItem('token');
    return !!token;
  }

  logout(){
    localStorage.removeItem('token');
    console.log("logged out");
  }
}