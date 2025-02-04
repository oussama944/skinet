import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layout/header/header.component";
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Pagination } from './shared/models/pagination';
import { Product } from './shared/models/product';
import { ApiResponse } from './shared/models/apiResponse';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit{
  baseUrl= 'https://localhost:5001/api/'
  private http = inject(HttpClient);
  title = 'Skinet';

  products: Product[]=[];

  ngOnInit(): void {
    this.http.get<ApiResponse<Pagination<Product>>>(this.baseUrl + 'products').subscribe({
      next: response => this.products = response.value.data,
      error: error => console.log(error),
      complete: () => console.log('complete'),
    });
  }
  
  
}
