import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductService, ProductDto } from '../../../../core/services/product.service';

@Component({
  selector: 'app-list-products',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './list-products.component.html',
  styleUrls: ['./list-products.component.css']
})
export class ListProductsComponent implements OnInit {
  products: ProductDto[] = [];
  isLoading = true;
  errorMessage: string = '';

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.productService.getAllProducts().subscribe({
      next: (data) => {
        this.products = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = err.message || 'Failed to load products';
        this.isLoading = false;
      }
    });
  }
}
