import { Component, Input, OnChanges } from '@angular/core';
import { WishlistService } from 'src/app/services/wishlist.service';
import { SubscriptionService } from 'src/app/services/subscription.service';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { Book } from 'src/app/models/book';

@Component({
  selector: 'app-addtowishlist',
  templateUrl: './addtowishlist.component.html',
  styleUrls: ['./addtowishlist.component.scss']
})
export class AddtowishlistComponent implements OnChanges {

  @Input()
  bookId: number;

  @Input()
  showButton = false;

  userId;
  toggle: boolean;
  buttonText: string;
  public wishlistItems: Book[];

  constructor(
    private wishlistService: WishlistService,
    private subscriptionService: SubscriptionService,
    private snackBarService: SnackbarService) {
    this.userId = localStorage.getItem('userId');
  }

  ngOnChanges() {
    this.subscriptionService.wishlistItem$.pipe().subscribe(
      (bookData: Book[]) => {
        this.setFavourite(bookData);
        this.setButtonText();
      });
  }

  setFavourite(bookData: Book[]) {
    const favBook = bookData.find(f => f.bookId === this.bookId);

    if (favBook) {
      this.toggle = true;
    } else {
      this.toggle = false;
    }
  }

  setButtonText() {
    if (this.toggle) {
      this.buttonText = 'Eliminar del Wishlist';
    } else {
      this.buttonText = 'Agregar al Wishlist';
    }
  }

  toggleValue() {
    this.toggle = !this.toggle;
    this.setButtonText();

    this.wishlistService.toggleWishlistItem(this.userId, this.bookId).subscribe(
      () => {
        if (this.toggle) {
          this.snackBarService.showSnackBar('Artículo agregado a su Wishlist');
        } else {
          this.snackBarService.showSnackBar('Artículo eliminado de su Wishlist');
        }
      }, error => {
        console.log('Ocurrió un error al agregar a su wishlist : ', error);
      });
  }
}
