import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CustomerService } from '../../../../core/services/customer.service';


@Component({
  selector: 'app-new-customer',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './new-customer.component.html',
  styleUrl: './new-customer.component.css'
})
export class NewCustomerComponent {
  customerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private customerService: CustomerService

  ) {
    this.customerForm = this.fb.group({
      name: ['', Validators.required],
      email: [''],
      phone: ['']
    });
  }

  onSubmit() {
    if (this.customerForm.valid) {
      const customer = this.customerForm.value;
      console.log('Saving customer:', customer);

      this.customerService.createCustomer(customer).subscribe({
        next: (res) => {
          alert('Customer created successfully!');
          this.customerForm.reset();
        },
        error: (err) => {
          console.error('Error creating customer:', err);
          alert('Failed to create customer. Please try again.');
        }
      });
    } else {
      alert('Please enter the required fields.');
    }
  }

}
