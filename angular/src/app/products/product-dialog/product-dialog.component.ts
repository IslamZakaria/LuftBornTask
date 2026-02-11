import { Component, Input, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ProductService } from '../../proxy/products/product.service';
import { ProductDto } from '../../proxy/products/models';
import { finalize } from 'rxjs/operators';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';

@Component({
    selector: 'app-product-dialog',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule, SharedModule],
    templateUrl: './product-dialog.component.html',
    styleUrl: './product-dialog.component.scss'
})
export class ProductDialogComponent implements OnInit {
    @Input() id?: string;

    form: FormGroup;
    isSaving = false;

    private fb = inject(FormBuilder);
    private activeModal = inject(NgbActiveModal);
    private productService = inject(ProductService);

    ngOnInit(): void {
        this.buildForm();
        if (this.id) {
            this.fetchData();
        }
    }

    buildForm() {
        this.form = this.fb.group({
            name: ['', [Validators.required, Validators.maxLength(128)]],
            price: [0, [Validators.required, Validators.min(0)]],
            description: ['', [Validators.maxLength(500)]],
        });
    }

    fetchData() {
        this.productService.get(this.id!).subscribe((response) => {
            this.form.patchValue(response);
        });
    }

    save() {
        if (this.form.invalid) {
            return;
        }

        this.isSaving = true;

        const request = this.id
            ? this.productService.update(this.id, this.form.value)
            : this.productService.create(this.form.value);

        request
            .pipe(finalize(() => (this.isSaving = false)))
            .subscribe(() => {
                this.activeModal.close(true);
            });
    }

    close() {
        this.activeModal.dismiss();
    }
}
