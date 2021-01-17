import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The Dating App';
  user: any;
constructor( private http: HttpClient)
{

}
ngOnInit(): void {
  this.getUser();
}

 getUser(): void {
  this.http.get('https://localhost:5001/api/users/').subscribe(response => {
    this.user = response;
  // tslint:disable-next-line:no-shadowed-variable
  }, error => {
    console.log(error);
  });
 }

}
