import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ProductListComponent } from './product-list.component';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { ProductService } from '../../proxy/products/product.service';
import { of } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService } from '@abp/ng.theme.shared';

describe('ProductListComponent', () => {
    let component: ProductListComponent;
    let fixture: ComponentFixture<ProductListComponent>;
    let mockProductService: jasmine.SpyObj<ProductService>;
    let mockListService: jasmine.SpyObj<ListService>;

    beforeEach(async () => {
        mockProductService = jasmine.createSpyObj('ProductService', ['getList', 'delete']);
        mockListService = jasmine.createSpyObj('ListService', ['hookToQuery', 'get']);
        const mockConfirmationService = jasmine.createSpyObj('ConfirmationService', ['warn']);
        const mockModalService = jasmine.createSpyObj('NgbModal', ['open']);

        await TestBed.configureTestingModule({
            imports: [ProductListComponent],
            providers: [
                { provide: ProductService, useValue: mockProductService },
                { provide: ListService, useValue: mockListService },
                { provide: ConfirmationService, useValue: mockConfirmationService },
                { provide: NgbModal, useValue: mockModalService },
            ]
        }).compileComponents();

        mockListService.hookToQuery.and.returnValue(of({ items: [], totalCount: 0 } as PagedResultDto<any>));

        fixture = TestBed.createComponent(ProductListComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should hook to query on init', () => {
        expect(mockListService.hookToQuery).toHaveBeenCalled();
    });
});
