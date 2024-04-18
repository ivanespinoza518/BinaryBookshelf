import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute, RouterLink, Router } from '@angular/router';
import { ReactiveFormsModule, FormGroup, FormBuilder, Validators, AbstractControl, AsyncValidatorFn } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { Category } from './category';
import { BaseFormComponent } from '../base-form.component';

@Component({
  selector: 'app-category-edit',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatButtonModule,
    MatSelectModule,
    MatInputModule,
    RouterLink
  ],
  templateUrl: './category-edit.component.html',
  styleUrl: './category-edit.component.scss'
})
export class CategoryEditComponent extends BaseFormComponent implements OnInit {
// the view title
  title?: string;

  // the category object to edit or create
  category?: Category;

  // the category id, as fetched from the active route:
  // It's NULL when we're adding a new category,
  // and not NULL when we're editing an existing one.
  id?: number;

  // the categories array for the select
  categories?: Category[];

  constructor(
    private fb: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private http: HttpClient) {
      super();
    }

  ngOnInit() {
    this.form = this.fb.group({
      label: ['',
        Validators.required,
        this.isDupeField("label")
      ]
    });

    this.loadData();
  }

  loadData() {
    // retrieve the ID from the 'id' parameter
    const idParam = this.activatedRoute.snapshot.paramMap.get('id');
    this.id = idParam ? +idParam : 0;
    if (this.id) {
      // EDIT MODE

      // fetch the category from the server
      const url = `${environment.baseUrl}Categories/${this.id}`;
      this.http.get<Category>(url).subscribe({
        next: (result) => {
          this.category = result;
          this.title = "Edit - " + this.category.label;

          // update the form with the category value
          this.form.patchValue(this.category);
        },
        error: (error) => console.error(error)
      });
    }
    else {
      // ADD NEW MODE

      this.title = "Create a new Category";
    }
  }

  onSubmit() {
    const category = (this.id) ? this.category : <Category>{};
    if (category) {
      category.label = this.form.controls['label'].value;
      
      if (this.id) {
        // EDIT MODE
        const url = `${environment.baseUrl}Categories/${category.id}`;
        this.http
          .put<Category>(url, category)
          .subscribe({
            next: (result) => {
              console.log("Category " + category!.id + " has been updated.");

              // go back to categories view
              this.router.navigate(['/categories']);
            },
            error: (error) => console.error(error)
          });
      }
      else {
        // ADD NEW mode
        const url = `${environment.baseUrl}Categories`;
        this.http
          .post<Category>(url, category)
          .subscribe({
            next: (result) => {
              console.log("Category " + result.id + " has been created.");

              // go back to categories view
              this.router.navigate(['/categories']);
            },
            error: (error) => console.error(error)
          });
      }
    }
  }

  isDupeField(fieldName: string): AsyncValidatorFn {
    return (control: AbstractControl): Observable<{
      [key: string]: any
    } | null> => {

      const params = new HttpParams()
        .set("categoryId", (this.id) ? this.id.toString() : "0")
        .set("fieldName", fieldName)
        .set("fieldValue", control.value);
      const url = `${environment.baseUrl}Categories/IsDupeField`;
      return this.http.post<boolean>(url, null, { params })
        .pipe(map(result => {
          return (result ? { isDupeField: true } : null);
        }));
    }
  }
}
