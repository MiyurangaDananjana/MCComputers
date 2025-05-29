import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductService, ProductDto } from '../../../../core/services/product.service';

@Component({
  selector: 'app-new-product',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './new-product.component.html',
  styleUrls: ['./new-product.component.css']
})
export class NewProductComponent {
  productForm: FormGroup;
  responseMessage: string = '';
  isSuccess: boolean = false;

  constructor(private fb: FormBuilder, private productService: ProductService) {
    this.productForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(500)]],
      stock: [0, [Validators.required, Validators.min(0)]]
    });
  }

  onSubmit(): void {
    if (this.productForm.invalid) return;

    const productData: ProductDto = this.productForm.value;

    this.productService.addProduct(productData).subscribe({
      next: () => {
        this.isSuccess = true;
        this.responseMessage = 'Product added successfully!';
        this.productForm.reset();
      },
      error: (err) => {
        this.isSuccess = false;
        this.responseMessage = err.error?.message || 'Error adding product.';
      }
    });
  }
}
