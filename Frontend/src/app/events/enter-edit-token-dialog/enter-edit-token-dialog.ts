import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  templateUrl: './enter-edit-token-dialog.html',
})
export class EnterEditTokenDialog {
  editToken: string | null = null;

  constructor(public readonly dialogRef: MatDialogRef<EnterEditTokenDialog>) {}
}
