import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface ProductDto {
  name: string;
  description: string;
  stock: number;
}

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'http://localhost:5001/api/Products';

  constructor(private http: HttpClient) { }

  addProduct(product: ProductDto): Observable<any> {
    return this.http.post(this.apiUrl, product);
  }

  getAllProducts(): Observable<ProductDto[]> {
    return this.http.get<ProductDto[]>(this.apiUrl);
  }

}
