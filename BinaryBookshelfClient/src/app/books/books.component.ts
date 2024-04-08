import { Component, OnInit, ViewChild } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { CurrencyPipe } from '@angular/common';

import { Book } from './book';

@Component({
  selector: 'app-books',
  standalone: true,
  imports: [
    MatTableModule,
    MatPaginator,
    CurrencyPipe
  ],
  templateUrl: './books.component.html',
  styleUrl: './books.component.scss'
})
export class BooksComponent implements OnInit {
  public displayedColumns: string[] = [ 'id', 'title', 'edition', 'isbn13', 'price' ];
  public books!: MatTableDataSource<Book>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private http: HttpClient) {
  }

  ngOnInit() {
    var pageEvent = new PageEvent();
    pageEvent.pageIndex = 0;
    pageEvent.pageSize = 10;
    this.getBooks(pageEvent);
  }

  getBooks(event: PageEvent) {
    //var url = environment.baseUrl + 'Books';
    var params = new HttpParams()
      .set("pageIndex", event.pageIndex.toString())
      .set("pageSize", event.pageSize.toString());

    this.http.get<any>(`${environment.baseUrl}Books`, { params })
      .subscribe({
        next: (result) => {
          this.paginator.length = result.totalCount;
          this.paginator.pageIndex = result.pageIndex;
          this.paginator.pageSize = result.pageSize;
          this.books = new MatTableDataSource<Book>(result.data);
        },
        error: (error) => console.error(error)
      });
  }
}
