import { Component, OnInit } from '@angular/core';
import Chart from 'chart.js';
import { StaticService } from 'app/services/static.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  constructor(
    private staticService: StaticService,
    private toastr: ToastrService, ) {
  }

  //bool vars for role access
  isLogin = (localStorage.getItem('token') != null);
  isManager = ((this.isLogin) && (localStorage.getItem('IsManager').toLocaleLowerCase() == 'true'));
  isCurrentUser = ((this.isLogin) && (localStorage.getItem('CurrentRole').toLocaleLowerCase() == 'false'));
  isEdit = (this.isManager && !this.isCurrentUser);
  userProfileId = localStorage.getItem('ClientId');
  //bool vars for role access

  //server data
  // chartData: any;
  chartData = {
    OrdersLastMonth: 0,
    PaymentLastMonth: 0,
    ClientsToday: 0,
    AverageRate: 0,
    TableOccupancyYesterday: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23],
    TableOccupancyToday: [0, 1, 6, 3, 4, 6, 6, 7, 6, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 6, 6, 6, 23],
    PerfectRateCount: 1,
    NormalRateCount: 1,
    BadRateCount: 1,
    ClientPaymentActivity: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11],
    VisitedLocationsLastMonth: 0
  }
  //server data


  public canvas: any;
  public ctx;
  public chartColor;
  public chartEmail;
  public chartHours;

  ngOnInit() {
    this.isLogin = (localStorage.getItem('token') != null);
    this.isManager = ((this.isLogin) && (localStorage.getItem('IsManager').toLocaleLowerCase() == 'true'));
    this.isCurrentUser = ((this.isLogin) && (localStorage.getItem('CurrentRole').toLocaleLowerCase() == 'false'));
    this.isEdit = (this.isManager && !this.isCurrentUser);
    this.chartColor = "#FFFFFF";
  }

  ngAfterViewInit() {
    this.loadChartData();
  }

  loadChartData() {
    if (this.isManager) {
      this.staticService.getChartDataManager().subscribe(
        (res: any) => {
          this.chartData = res;
          this.drawClientChart();
          if (this.isManager) {
            this.drawManagerChart();
          }
        },
        err => {
          console.log(err);
          this.toastr.error(err.error, 'Error');
        }
      );
    }
    else {
      this.staticService.getChartDataClient().subscribe(
        (res: any) => {
          this.chartData = res;
          this.drawClientChart();
          if (this.isManager) {
            this.drawManagerChart();
          }
        },
        err => {
          console.log(err);
          this.toastr.error(err.error, 'Error');
        }
      );
    }
  }

  drawManagerChart() {
    var speedCanvas = document.getElementById("speedChart");

    var dataFirst = {
      data: this.chartData.TableOccupancyToday,
      // data: [0, 19, 15, 20, 30, 40, 40, 50, 25, 30, 50, 70],
      fill: false,
      borderColor: '#fbc658',
      backgroundColor: 'transparent',
      pointBorderColor: '#fbc658',
      pointRadius: 4,
      pointHoverRadius: 4,
      pointBorderWidth: 8,
    };

    var dataSecond = {
      data: this.chartData.TableOccupancyYesterday,
      // data: [0, 5, 10, 12, 20, 27, 30, 34, 42, 45, 55, 63],
      fill: false,
      borderColor: '#51CACF',
      backgroundColor: 'transparent',
      pointBorderColor: '#51CACF',
      pointRadius: 4,
      pointHoverRadius: 4,
      pointBorderWidth: 8
    };

    var speedData = {
      labels: ["00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"],
      datasets: [dataFirst, dataSecond]
    };

    var chartOptions = {
      legend: {
        display: false,
        position: 'top'
      }
    };

    var lineChart = new Chart(speedCanvas, {
      type: 'line',
      hover: false,
      data: speedData,
      options: chartOptions
    });
  }

  drawClientChart() {
    this.canvas = document.getElementById("chartHours");
    this.ctx = this.canvas.getContext("2d");

    this.chartHours = new Chart(this.ctx, {
      type: 'line',

      data: {
        labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct"],
        datasets: [{
          borderColor: "#6bd098",
          backgroundColor: "#6bd098",
          pointRadius: 0,
          pointHoverRadius: 0,
          borderWidth: 3,
          data: this.chartData.ClientPaymentActivity
          // data: [300, 310, 316, 322, 330, 326, 333, 345, 338, 354]
        }
        ]
      },
      options: {
        legend: {
          display: false
        },

        tooltips: {
          enabled: false
        },

        scales: {
          yAxes: [{

            ticks: {
              fontColor: "#9f9f9f",
              beginAtZero: false,
              maxTicksLimit: 5,
              //padding: 20
            },
            gridLines: {
              drawBorder: false,
              zeroLineColor: "#ccc",
              color: 'rgba(255,255,255,0.05)'
            }

          }],

          xAxes: [{
            barPercentage: 1.6,
            gridLines: {
              drawBorder: false,
              color: 'rgba(255,255,255,0.1)',
              zeroLineColor: "transparent",
              display: false,
            },
            ticks: {
              padding: 20,
              fontColor: "#9f9f9f"
            }
          }]
        },
      }
    });


    this.canvas = document.getElementById("chartEmail");
    this.ctx = this.canvas.getContext("2d");
    this.chartEmail = new Chart(this.ctx, {
      type: 'pie',
      data: {
        labels: [1, 2, 3],
        datasets: [{
          label: "Emails",
          pointRadius: 0,
          pointHoverRadius: 0,
          backgroundColor: [
            '#313131',
            '#4acccd',
            '#fcc468'
          ],
          borderWidth: 0,
          data: [this.chartData.PerfectRateCount, this.chartData.NormalRateCount, this.chartData.BadRateCount]
          // data: [342, 480, 530, 120]
        }]
      },

      options: {

        legend: {
          display: false
        },

        pieceLabel: {
          render: 'percentage',
          fontColor: ['white'],
          precision: 2
        },

        tooltips: {
          enabled: false
        },

        scales: {
          yAxes: [{

            ticks: {
              display: false
            },
            gridLines: {
              drawBorder: false,
              zeroLineColor: "transparent",
              color: 'rgba(255,255,255,0.05)'
            }

          }],

          xAxes: [{
            barPercentage: 1.6,
            gridLines: {
              drawBorder: false,
              color: 'rgba(255,255,255,0.1)',
              zeroLineColor: "transparent"
            },
            ticks: {
              display: false,
            }
          }]
        },
      }
    });
  }
}
