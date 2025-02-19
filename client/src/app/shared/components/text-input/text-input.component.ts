
import { Component, Input, Self } from '@angular/core';
import {
  ControlValueAccessor,
  FormControl,
  NgControl,
  ReactiveFormsModule,
} from '@angular/forms';
import { MatButtonModule, MatIconButton } from '@angular/material/button';
import { MatError, MatFormField, MatLabel, MatSuffix } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInput } from '@angular/material/input';

@Component({
  selector: 'app-text-input',
  standalone: true,
  imports: [ReactiveFormsModule, MatFormField, MatInput, MatError, MatLabel, MatButtonModule, MatIconModule, MatIconButton,MatSuffix],
  templateUrl: './text-input.component.html',
  styleUrl: './text-input.component.scss',
})
export class TextInputComponent implements ControlValueAccessor {
  @Input() label = '';
  @Input() type = 'text';
  hide = true; // Add a variable to toggle password visibility
  private timeoutId: any = null; // Stocke le timer

  constructor(@Self() public controlDir: NgControl) {
    this.controlDir.valueAccessor = this;
  }
  writeValue(obj: any): void {

  }
  registerOnChange(fn: any): void {

  }
  registerOnTouched(fn: any): void {

  }

  get control() {
    return this.controlDir.control as FormControl;
  }

  isPasswordField(): boolean {
    return this.type === 'password';
  }

  togglePassword(e: Event): void {
    e.preventDefault();
    this.hide = !this.hide;

    // Supprime le timer existant s'il y en a un
    if (this.timeoutId) {
      clearTimeout(this.timeoutId);
      this.timeoutId = null;
    }

    // Si le champ est visible, on déclenche un timer pour masquer après 5 sec
    if (!this.hide) {
      this.timeoutId = setTimeout(() => {
        this.hide = true;
        this.timeoutId = null;
      }, 5000);
    }
  }

  getPatternErrorMessage(): string {
    const value = this.control.value as string;
    if (!value) return 'Password is required';
  
    if (value.length < 6) {
      return 'Password must be at least 6 characters long';
    }
    if (!/[A-Z]/.test(value)) {
      return 'Password must contain at least one uppercase letter';
    }
    if (!/[0-9]/.test(value)) {
      return 'Password must contain at least one digit';
    }
    if (!/[^a-zA-Z0-9]/.test(value)) {
      return 'Password must contain at least one special character';
    }
  
    return 'Password does not meet the requirements';
  }
  

}
