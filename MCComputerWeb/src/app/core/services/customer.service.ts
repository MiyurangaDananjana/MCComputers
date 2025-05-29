import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private http: HttpClient, private router: Router) { }

  private apiUrl = 'http://localhost:5001/api/customers';


  createCustomer(customer: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, customer).pipe(
      tap(response => {
        console.log('Customer created:', response);
      })
    );
  }

  getAllCustomers(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
}
