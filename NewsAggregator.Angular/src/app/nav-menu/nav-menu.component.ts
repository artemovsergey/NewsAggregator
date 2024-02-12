import { Component } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrl: './nav-menu.component.css'
})
export class NavMenuComponent {
  isLoadingHabr = false;
  isLoadingLenta = false;
  constructor(private http: HttpClient, private router: Router,) { }


  parseHabr() {
    this.isLoadingHabr = true;
    this.http.get('http://localhost:8080/api/news/habrss').subscribe(
      (data) => {
        // Обработка полученных данных
        console.log(data);
        this.isLoadingHabr = false;
        location.reload();
      },
      (error) => {
        // Обработка ошибки
        console.error(error);
        this.isLoadingHabr = false;
      }
    );

  }

  parseLenta() {
    this.isLoadingLenta = true;
    this.http.get('http://localhost:8080/api/news/fetchdata').subscribe(
      (data) => {
        // Обработка полученных данных
        console.log(data);
        this.isLoadingLenta = false;
        location.reload();
      },
      (error) => {
        // Обработка ошибки
        console.error(error);
        this.isLoadingLenta = false;
      }
    );

  }


}
