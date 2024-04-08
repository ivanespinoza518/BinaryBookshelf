import { Component, OnInit } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { environment } from './../../environments/environment';
import { MatTableModule } from '@angular/material/table';
import { CurrencyPipe } from '@angular/common';

import { Book } from './book';

@Component({
  selector: 'app-books',
  standalone: true,
  imports: [
    MatTableModule,
    CurrencyPipe
  ],
  templateUrl: './books.component.html',
  styleUrl: './books.component.scss'
})
export class BooksComponent implements OnInit {
  public books: Book[] = [];
  public displayedColumns: string[] = [
    'id',
    'title',
    'edition',
    'isbn13',
    'price'
  ]

  constructor(private http: HttpClient) {
  }

  ngOnInit() {
    this.getBooks();
  }

  getBooks() {
    this.http.get<Book[]>(`${environment.baseUrl}Books`).subscribe(
      {
        next: (result) => this.books = result,
        error: (error) => console.error(error)
      }
    );
  }
}
