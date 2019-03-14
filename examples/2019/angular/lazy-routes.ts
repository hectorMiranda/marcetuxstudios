// app-routing.module.ts
// Each feature module loads only when its route is first activated.
// The Angular CLI splits output into separate chunks automatically.
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/auth.guard';

const routes: Routes = [
  { path: '', redirectTo: 'accounts', pathMatch: 'full' },
  {
    path: 'accounts',
    loadChildren: () =>
      import('./features/accounts/accounts.module').then(m => m.AccountsModule),
    canActivate: [AuthGuard],
  },
  {
    path: 'payments',
    loadChildren: () =>
      import('./features/payments/payments.module').then(m => m.PaymentsModule),
    canActivate: [AuthGuard],
  },
  {
    path: 'reports',
    loadChildren: () =>
      import('./features/reports/reports.module').then(m => m.ReportsModule),
    canActivate: [AuthGuard],
  },
  {
    path: 'admin',
    loadChildren: () =>
      import('./features/admin/admin.module').then(m => m.AdminModule),
    canActivate: [AuthGuard],
    // data: { roles: ['BankAdmin'] } — role check inside the guard
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
