import { Component } from '@angular/core';
import { BookService } from './book.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  books: string[];
  constructor(private bookService: BookService){
    this.books = [];
    this.fetchBooks();
  }

  fetchBooks(){
    this.bookService.getBooks().subscribe(res => {
      this.books = res;
    })
  }
}
