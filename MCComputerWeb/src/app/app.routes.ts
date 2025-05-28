import { Routes } from '@angular/router';
import { LoginComponent } from './shared/features/auth/login/login.component';
//import { AuthGuard } from './core/guards/auth.guard';

export const routes: Routes = [
    // Redirect root path to /login
    { path: '', redirectTo: 'login', pathMatch: 'full' },

    // Login route
    { path: 'login', component: LoginComponent },

    // // Protected routes
    // {
    //     path: 'invoices',
    //     canActivate: [AuthGuard],
    //     loadChildren: () => import('./features/invoices/invoices.routes').then(m => m.routes)
    // },

    // Wildcard route to handle unknown paths
    { path: '**', redirectTo: 'login' }
];
