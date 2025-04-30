import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {

  constructor(private snackBar: MatSnackBar) { }

  showError(message: string): void {
    this.snackBar.open(message, 'Kapat', {
      duration: 5000,
      horizontalPosition: 'end', // Snackbar'ın yatay (x) pozisyonunu belirler
      verticalPosition: 'bottom', // Snackbar'ın dikey (y) pozisyonunu belirler
    });
  }
}
