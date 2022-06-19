import { Component } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';

import { IdentityService } from '../shared/api/services';

@Component({
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  registerFormGroup: FormGroup;
  loading = false;
  error = false;

  constructor(
    private readonly identityService: IdentityService,
    private readonly router: Router,
    readonly formBuilder: FormBuilder
  ) {
    this.registerFormGroup = formBuilder.group(
      {
        email: ['', [Validators.required, Validators.email]],
        username: [
          '',
          [
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(20),
            Validators.pattern(/^[a-zA-Z]*$/),
          ],
        ],
        password: [
          '',
          [
            Validators.required,
            Validators.minLength(8),
            Validators.pattern(/(?=.*\d)(?=.*[a-z])(?=.*[A-Z])/),
          ],
        ],
        confirmPassword: ['', Validators.required],
      },
      { validators: RegisterComponent.comparePasswords }
    );
  }

  submitForm(): void {
    this.loading = true;
    this.error = false;
    this.identityService
      .identityRegister({
        body: {
          email: this.registerFormGroup.get('email')!.value,
          username: this.registerFormGroup.get('username')!.value,
          password: this.registerFormGroup.get('password')!.value,
        },
      })
      .subscribe({
        next: () => {
          this.loading = false;
          this.router.navigate(['/login']);
        },
        error: () => {
          this.loading = false;
          this.error = true;
        },
      });
  }

  private static comparePasswords: ValidatorFn = (
    formGroup: AbstractControl
  ): ValidationErrors | null => {
    return formGroup.get('password')?.value ===
      formGroup.get('confirmPassword')?.value
      ? null
      : { passwordsDifferent: true };
  };
}
