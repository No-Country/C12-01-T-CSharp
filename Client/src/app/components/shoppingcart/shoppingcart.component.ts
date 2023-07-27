import { Component, OnInit, OnDestroy } from '@angular/core';
import { ShoppingCart } from 'src/app/models/shoppingcart';
import { CartService } from 'src/app/services/cart.service';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { SubscriptionService } from 'src/app/services/subscription.service';

@Component({
  selector: 'app-shoppingcart',
  templateUrl: './shoppingcart.component.html',
  styleUrls: ['./shoppingcart.component.scss']
})
export class ShoppingcartComponent implements OnInit, OnDestroy {
  public cartItems: ShoppingCart[];
  userId;
  totalPrice: number;
  private unsubscribe$ = new Subject<void>();
  isLoading: boolean;

  constructor(
    private cartService: CartService,
    private snackBarService: SnackbarService,
    private subscriptionService: SubscriptionService) {
    this.userId = localStorage.getItem('userId');
  }

  ngOnInit() {
    this.cartItems = [];
    this.isLoading = true;
    this.getShoppingCartItems();
  }

  getShoppingCartItems() {
    this.cartService.getCartItems(this.userId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        (result: ShoppingCart[]) => {
          this.cartItems = result;
          this.getTotalPrice();
          this.isLoading = false;
        }, error => {
          console.log('Ocurrió un error al obtener el artículo del carrito de compras: ', error);
        });
  }

  getTotalPrice() {
    this.totalPrice = 0;
    this.cartItems.forEach(item => {
      this.totalPrice += (item.book.price * item.quantity);
    });
  }

  deleteCartItem(bookId: number) {
    this.cartService.removeCartItems(this.userId, bookId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        result => {
          this.subscriptionService.cartItemcount$.next(result);
          this.snackBarService.showSnackBar('Producto eliminado del carrito');
          this.getShoppingCartItems();
        }, error => {
          console.log('Ocurrió un error al eliminar el artículo del carrito: ', error);
        });
  }

  addToCart(bookId: number) {
    this.cartService.addBookToCart(this.userId, bookId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        result => {
          this.subscriptionService.cartItemcount$.next(result);
          this.snackBarService.showSnackBar('Un artículo añadido al carrito');
          this.getShoppingCartItems();
        }, error => {
          console.log('Ocurrió un error al agregar datos al carrito: ', error);
        });
  }

  deleteOneCartItem(bookId: number) {
    this.cartService.deleteOneCartItem(this.userId, bookId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        result => {
          this.subscriptionService.cartItemcount$.next(result);
          this.snackBarService.showSnackBar('Un artículo eliminado del carrito');
          this.getShoppingCartItems();
        }, error => {
          console.log('Se produjo un error al obtener los datos del libro: ', error);
        });
  }

  clearCart() {
    this.cartService.clearCart(this.userId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        result => {
          this.subscriptionService.cartItemcount$.next(result);
          this.snackBarService.showSnackBar('Carrito limpio!!!');
          this.getShoppingCartItems();
        }, error => {
          console.log('Ocurrió un error al eliminar el artículo del carrito: ', error);
        });
  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
