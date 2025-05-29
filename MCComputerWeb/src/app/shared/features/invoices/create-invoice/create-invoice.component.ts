import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { InvoiceService } from '../../../../core/services/invoice.service';
import { CustomerService } from '../../../../core/services/customer.service';
import { Invoice, InvoiceItem } from '../../../../models/invoice.model';
import { Customer } from '../../../../models/customer.model';

@Component({
  selector: 'app-create-invoice',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './create-invoice.component.html',
  styleUrl: './create-invoice.component.css'
})
export class CreateInvoiceComponent implements OnInit {
  invoice: Invoice = {
    customerId: 0,
    totalAmmount: 0,
    invoiceItems: []
  };

  newItem: InvoiceItem = {
    productId: 0,
    quantity: 1,
    price: 0
  };

  customers: Customer[] = [];
  filteredCustomers: Customer[] = [];
  searchTerm: string = '';

  constructor(
    private invoiceService: InvoiceService,
    private customerService: CustomerService
  ) { }

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers() {
    this.customerService.getAllCustomers().subscribe({
      next: (data) => {
        this.customers = data;
        this.filteredCustomers = data;
      },
      error: (err) => {
        console.error('Failed to load customers:', err);
      }
    });
  }

  filterCustomers() {
    const term = this.searchTerm.toLowerCase();
    this.filteredCustomers = this.customers.filter(c =>
      c.name.toLowerCase().includes(term)
    );
    this.setCustomerId(); // Call after filtering
  }

  setCustomerId() {
    const selected = this.customers.find(c => c.name.toLowerCase() === this.searchTerm.toLowerCase());
    if (selected) {
      this.invoice.customerId = selected.customerId;
    } else {
      this.invoice.customerId = 0;
    }
  }

  addItem() {
    if (this.newItem.productId && this.newItem.quantity > 0 && this.newItem.price >= 0) {
      this.invoice.invoiceItems.push({ ...this.newItem });
      this.newItem = { productId: 0, quantity: 1, price: 0 };
      this.calculateTotal();
    }
  }

  calculateTotal() {
    this.invoice.totalAmmount = this.invoice.invoiceItems.reduce(
      (sum, item) => sum + item.quantity * item.price,
      0
    );
  }

  submitInvoice() {
    this.invoiceService.createInvoice(this.invoice).subscribe({
      next: () => {
        alert('Invoice created!');
        this.invoice = { customerId: 0, totalAmmount: 0, invoiceItems: [] };
      },
      error: (err) => {
        console.error(err);
        alert('Failed to create invoice.');
      }
    });
  }
}
