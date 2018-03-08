import { Component, OnInit, Inject } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  books: string[];
  constructor(@Inject('libraryService') private libraryService) {
    this.books = [];
    this.fetchBooks();
  }

  fetchBooks() {
    this.libraryService.getBooks().subscribe(res => {
      this.books = res;
    });
  }

  ngOnInit() {
  }

}
