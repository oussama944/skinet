import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function passwordValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const password = control.value;

    if (!password) {
      return null; // Allow empty passwords if the field is not required.  Use a separate `required` validator if needed.
    }

    const hasMinLength = password.length >= 6;
    const hasDigit = /[0-9]/.test(password);
    const hasUppercase = /[A-Z]/.test(password);
    const hasNonAlphanumeric = /[^a-zA-Z0-9]/.test(password);

    const errors: ValidationErrors = {};

    if (!hasMinLength) {
      errors['minlength'] = true;
    }

    if (!hasDigit) {
      errors['digit'] = true;
    }

    if (!hasUppercase) {
      errors['uppercase'] = true;
    }

    if (!hasNonAlphanumeric) {
      errors['nonAlphanumeric'] = true;
    }

    return Object.keys(errors).length === 0 ? null : errors;
  };
}