import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Book } from 'src/app/models/book';
import { BookService } from 'src/app/services/book.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  styleUrls: ['./book-form.component.scss']
})
export class BookFormComponent implements OnInit, OnDestroy {

  private formData = new FormData();
  bookForm: FormGroup;
  book: Book = new Book();
  formTitle = 'Agregar';
  coverImagePath;
  coverFileContent;
  bookId;
  files;
  categoryList: [];
  private unsubscribe$ = new Subject<void>();

  constructor(
    private bookService: BookService,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private router: Router) {

    this.bookForm = this.fb.group({
      bookId: 0,
      title: ['', Validators.required],
      author: ['', Validators.required],
      category: ['', Validators.required],
      price: ['', [Validators.required, Validators.min(0)]],
      coverImagePath: this.coverImagePath,
      coverFileContent: this.coverFileContent,
    });
  }

  get title() {
    return this.bookForm.get('title');
  }

  get author() {
    return this.bookForm.get('author');
  }

  get category() {
    return this.bookForm.get('category');
  }

  get price() {
    return this.bookForm.get('price');
  }


  ngOnInit() {
    this.bookService.categories$
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        (categoryData: []) => {
          this.categoryList = categoryData;
        }, error => {
          console.log('Error ocurred while fetching category List : ', error);
        });

    this.route.params.subscribe(
      params => {
        if (params.id) {
          this.bookId = +params.id;
          this.fetchBookData();
        }
      }
    );
  }

  fetchBookData() {
    this.formTitle = 'Editar';
    this.bookService.getBookById(this.bookId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        (result: Book) => {
          this.setBookFormData(result);
        }, error => {
          console.log('Error ocurred while fetching book data : ', error);
        });
  }

  onFormSubmit() {
    if (!this.bookForm.valid) {
      return;
    }
    if (this.files && this.files.length > 0) {
      for (let j = 0; j < this.files.length; j++) {
        this.formData.append('file' + j, this.files[j]);
      }
    }
    this.bookForm.value.coverFileContent = this.coverFileContent;
    this.formData.append('bookFormData', JSON.stringify(this.bookForm.value));

    if (this.bookId) {
      this.editBookDetails();
    } else {
      this.saveBookDetails();
    }
  }


  editBookDetails() {
    let bookData = this.formData.get('bookFormData');
    let bookDataJson = JSON.stringify(bookData);
    bookDataJson = JSON.parse(bookDataJson);
    bookDataJson = JSON.parse(bookDataJson);
    const BOOKID = bookDataJson['bookId'];
    this.bookService.updateBookDetails(BOOKID, bookDataJson)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        () => {
          this.router.navigate(['/admin/books']);
        }, error => {
          console.log('Error ocurred while updating book data : ', error);
        });
  }

  saveBookDetails() {
    let bookData = this.formData.get('bookFormData');
    let bookDataJson = JSON.stringify(bookData);
    bookDataJson = JSON.parse(bookDataJson);
    bookDataJson = JSON.parse(bookDataJson);
    this.bookService.addBook(bookDataJson)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        () => {
          this.router.navigate(['/admin/books']);
        }, error => {
          this.bookForm.reset();
          console.log('Error ocurred while adding book data : ', error);
        });
  }

  cancel() {
    this.router.navigate(['/admin/books']);
  }

  setBookFormData(bookFormData) {
    this.bookForm.setValue({
      bookId: bookFormData.bookId,
      title: bookFormData.title,
      author: bookFormData.author,
      category: bookFormData.category,
      price: bookFormData.price,
      coverImagePath: bookFormData.coverFileName,
      coverFileContent: bookFormData.coverFileName
    });
    this.coverImagePath = bookFormData.coverFileName;
  }

  uploadImage(event) {
    this.files = event.target.files;
    const reader = new FileReader();
    reader.readAsDataURL(event.target.files[0]);
    reader.onload = (myevent: ProgressEvent) => {
      this.coverImagePath = (myevent.target as FileReader).result;
      this.coverFileContent = this.coverImagePath.replace('data:', '').replace(/^.+,/, '');
    };
  }



  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
