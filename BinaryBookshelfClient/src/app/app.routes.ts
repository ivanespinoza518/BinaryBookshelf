import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { BooksComponent } from './books/books.component';
import { AuthorsComponent } from './authors/authors.component';
import { CategoriesComponent } from './categories/categories.component';

export const routes: Routes = [
    { path: '', component: HomeComponent, pathMatch : 'full' },
    { path: 'books', component: BooksComponent },
    { path: 'authors', component: AuthorsComponent },
    { path: 'categories', component: CategoriesComponent }
];
