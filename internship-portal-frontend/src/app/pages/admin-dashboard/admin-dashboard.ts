import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseChartDirective } from 'ng2-charts';
import { AdminService } from '../../services/admin.service';
import { ChartConfiguration, ChartType } from 'chart.js';
import { Chart, registerables } from 'chart.js';
import { ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';

Chart.register(...registerables);

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, BaseChartDirective],
  templateUrl: './admin-dashboard.html'
})
export class AdminDashboard implements OnInit {

  stats: any;

  barChartType: ChartType = 'bar';

  barChartData: ChartConfiguration['data'] = {
    labels: ['Internships', 'Applications', 'Accepted'],
    datasets: [
      {
        data: [],
        label: 'Portal Statistics'
      }
    ]
  };

  pieChartType: ChartType = 'pie';

  pieChartData: ChartConfiguration['data'] = {
    labels: ['Accepted', 'Rejected/Pending'],
    datasets: [
      {
        data: [],
        backgroundColor: [
        '#4CAF50',   // green for accepted
        '#F44336'    // red for rejected
      ]
      }
    ]
  };

departmentChartType: ChartType = 'bar';

departmentChartData: ChartConfiguration['data'] = {
  labels: [],
  datasets: [
    {
      label: 'Applications by Department',
      data: [],
      backgroundColor: '#2196F3'
    }
  ]
};

  constructor(private adminService: AdminService,private cd: ChangeDetectorRef,private router : Router) {}

  ngOnInit() {

    this.adminService.getStats().subscribe({
      next: (data: any) => {

        this.stats = data;

        this.barChartData.datasets[0].data = [
          data.totalInternships,
          data.totalApplications,
          data.acceptedApplications
        ];

        this.pieChartData.datasets[0].data = [
          data.acceptedApplications,
          data.totalApplications - data.acceptedApplications
        ];

         this.cd.detectChanges(); 
      },
      error: (err) => console.error(err)
    });

    this.adminService.getApplicationsByDepartment().subscribe({
  next: (data: any) => {

    const labels = data.map((d: any) => d.department);
    const values = data.map((d: any) => d.count);

    this.departmentChartData.labels = labels;
    this.departmentChartData.datasets[0].data = values;

    this.cd.detectChanges();
  },
  error: (err) => console.error(err)
});

  }

viewInternships() {
  this.router.navigate(['/internships']);
}

logout() {

  localStorage.removeItem('token');

  this.router.navigate(['/']);

}


}
