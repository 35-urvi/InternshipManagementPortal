import { Routes } from '@angular/router';
import { InternshipList } from './pages/internship-list/internship-list';
import { Login } from './pages/login/login';
import { StudentDashboard } from './pages/student-dashboard/student-dashboard';
import { CompanyDashboard } from './pages/company-dashboard/company-dashboard';
import { AdminDashboard } from './pages/admin-dashboard/admin-dashboard';
import { Register } from './pages/register/register';
import { RegisterStudent } from './pages/register-student/register-student';
import { RegisterCompany } from './pages/register-company/register-company';
import { CreateInternship } from './pages/create-internship/create-internship';


export const routes: Routes = [
  // { path: '', component: InternshipList },
  { path: '', component: Login },
  { path: 'student-dashboard', component: StudentDashboard },
  { path: 'company-dashboard', component: CompanyDashboard },
  { path: 'admin-dashboard', component: AdminDashboard },
  { path: 'register-student', component: RegisterStudent },
  { path: 'register-company', component: RegisterCompany },
  { path: 'register', component: Register },
  { path: 'create-internship', component: CreateInternship },
  { path: 'internships', component: InternshipList }  
];
