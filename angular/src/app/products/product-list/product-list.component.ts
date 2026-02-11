import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit, inject } from '@angular/core';
import { ProductService } from '../../proxy/products/product.service';
import { ProductDto } from '../../proxy/products/models';
import { NgbModal, NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { ProductDialogComponent } from '../product-dialog/product-dialog.component';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';

@Component({
    selector: 'app-product-list',
    standalone: true,
    imports: [CommonModule, SharedModule, NgbDropdownModule],
    providers: [ListService],
    templateUrl: './product-list.component.html',
    styleUrl: './product-list.component.scss'
})
export class ProductListComponent implements OnInit {
    product = { items: [], totalCount: 0 } as PagedResultDto<ProductDto>;

    private productService = inject(ProductService);
    private confirmation = inject(ConfirmationService);
    private modalService = inject(NgbModal);
    public readonly list = inject(ListService);

    ngOnInit() {
        const productStreamCreator = (query) => this.productService.getList(query);

        this.list.hookToQuery(productStreamCreator).subscribe((response) => {
            this.product = response;
        });
    }

    createProduct() {
        const modalRef = this.modalService.open(ProductDialogComponent);
        modalRef.result.then((result) => {
            if (result) {
                this.list.get();
            }
        }, () => { });
    }

    editProduct(id: string) {
        const modalRef = this.modalService.open(ProductDialogComponent);
        modalRef.componentInstance.id = id;
        modalRef.result.then((result) => {
            if (result) {
                this.list.get();
            }
        }, () => { });
    }

    deleteProduct(id: string) {
        this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe((status) => {
            if (status === Confirmation.Status.confirm) {
                this.productService.delete(id).subscribe(() => this.list.get());
            }
        });
    }
}
