import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ProductDialogComponent } from './product-dialog.component';
import { ProductService } from '../../proxy/products/product.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule } from '@angular/forms';
import { of } from 'rxjs';

describe('ProductDialogComponent', () => {
    let component: ProductDialogComponent;
    let fixture: ComponentFixture<ProductDialogComponent>;
    let mockProductService: jasmine.SpyObj<ProductService>;
    let mockActiveModal: jasmine.SpyObj<NgbActiveModal>;

    beforeEach(async () => {
        mockProductService = jasmine.createSpyObj('ProductService', ['get', 'create', 'update']);
        mockActiveModal = jasmine.createSpyObj('NgbActiveModal', ['close', 'dismiss']);

        await TestBed.configureTestingModule({
            imports: [ProductDialogComponent, ReactiveFormsModule],
            providers: [
                { provide: ProductService, useValue: mockProductService },
                { provide: NgbActiveModal, useValue: mockActiveModal }
            ]
        }).compileComponents();

        fixture = TestBed.createComponent(ProductDialogComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should initialize form', () => {
        expect(component.form).toBeDefined();
        expect(component.form.controls['name']).toBeDefined();
        expect(component.form.controls['price']).toBeDefined();
    });

    it('should not save if form is invalid', () => {
        component.save();
        expect(mockProductService.create).not.toHaveBeenCalled();
    });

    it('should call create when id is not present', () => {
        component.form.patchValue({
            name: 'Test Product',
            price: 10,
            description: 'Test'
        });

        mockProductService.create.and.returnValue(of({} as any));

        component.save();

        expect(mockProductService.create).toHaveBeenCalled();
        expect(mockActiveModal.close).toHaveBeenCalled();
    });
});
