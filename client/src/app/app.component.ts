import { CommonModule, NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,NgFor,CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
 
  http = inject(HttpClient);
  title = 'Dating App';
  users:any;

  ngOnInit(): void {
    this.http.get("https://localhost:7064/api/Users").subscribe({
      next:response=>this.users= response,
      error:error=>console.log(error),
      complete:()=>console.log("Request has completed")
    })
  }


}
