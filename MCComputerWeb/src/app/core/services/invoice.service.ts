import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Invoice } from '../../models/invoice.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {
  private baseUrl = 'http://localhost:5001/api/invoices'; // Replace with your actual URL

  constructor(private http: HttpClient) { }

  createInvoice(invoice: Invoice): Observable<Invoice> {
    console.log("invoice Details " + JSON.stringify(invoice));
    return this.http.post<Invoice>(this.baseUrl, invoice);
  }

}
