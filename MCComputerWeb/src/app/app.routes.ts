import { Routes } from '@angular/router';
import { LoginComponent } from './shared/features/auth/login/login.component';
import { AuthGuard } from './core/guards/auth.guard';
import { HomeComponent } from './shared/features/home/home.component';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },

  { path: 'login', component: LoginComponent },

  {
    path: 'home',
    component: HomeComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'new-customer',
        loadComponent: () =>
          import('./shared/features/customer/new-customer/new-customer.component').then(
            (m) => m.NewCustomerComponent
          )
      },
      {
        path: 'list-customers',
        loadComponent: () =>
          import('./shared/features/customer/list-customers/list-customers.component').then(
            (m) => m.ListCustomersComponent
          )
      },
      {
        path: 'new-product',
        loadComponent: () =>
          import('./shared/features/products/new-product/new-product.component').then(
            (m) => m.NewProductComponent
          )
      },
      {
        path: 'list-products',
        loadComponent: () =>
          import('./shared/features/products/list-products/list-products.component').then(
            (m) => m.ListProductsComponent
          )
      },
      {
        path: 'create-invoice',
        loadComponent: () =>
          import('./shared/features/invoices/create-invoice/create-invoice.component').then(
            (m) => m.CreateInvoiceComponent
          )
      },
      { path: '', redirectTo: 'invoices', pathMatch: 'full' }
    ]
  },
  { path: '**', redirectTo: 'login' }
];
