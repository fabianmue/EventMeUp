import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs';

import { AuthenticationService } from '../shared/services/authentication.service';

@Component({
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class LoginComponent implements OnInit {
  loginFormGroup: FormGroup;
  loading = false;
  error: { code: number } | null = null;

  constructor(
    private readonly authenticationService: AuthenticationService,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    readonly formBuilder: FormBuilder
  ) {
    this.loginFormGroup = formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  ngOnInit(): void {}

  submitForm(): void {
    if (this.loginFormGroup.invalid) {
      return;
    }

    this.loading = true;
    this.authenticationService
      .login(
        this.loginFormGroup.get('email')?.value,
        this.loginFormGroup.get('password')?.value
      )
      .pipe(first())
      .subscribe({
        next: () => {
          this.loading = false;
          const returnUrl = this.route.snapshot.queryParams['returnUrl'] ?? '/';
          this.router.navigate([returnUrl]);
        },
        error: (error) => {
          this.loading = false;
          this.error = { code: 500 };
        },
      });
  }
}
