import { Routes } from '@angular/router';
import { CalculatorComponent } from './pages/calculator/calculator.component';
import { ManageOperationsComponent } from './pages/manage-operations/manage-operations.component';

export const routes: Routes = [
  { path: '', redirectTo: '/calculator', pathMatch: 'full' },
  { path: 'calculator', component: CalculatorComponent,title: 'Gaia Project - Calculator'},
  { path: 'manage', component: ManageOperationsComponent, title: 'Gaia Project - Manage Operations' },
  { path: '**', redirectTo: '/calculator' }
];
