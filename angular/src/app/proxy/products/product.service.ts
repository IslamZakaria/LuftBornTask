import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import { CreateUpdateProductDto, GetProductsInput, ProductDto } from './models';
import { PagedResultDto } from '@abp/ng.core';

@Injectable({
    providedIn: 'root',
})
export class ProductService {
    apiName = 'Default';

    constructor(private restService: RestService) { }

    create = (input: CreateUpdateProductDto) =>
        this.restService.request<any, ProductDto>({
            method: 'POST',
            url: '/api/app/product',
            body: input,
        });

    delete = (id: string) =>
        this.restService.request<any, void>({
            method: 'DELETE',
            url: `/api/app/product/${id}`,
        });

    get = (id: string) =>
        this.restService.request<any, ProductDto>({
            method: 'GET',
            url: `/api/app/product/${id}`,
        });

    getList = (input: GetProductsInput) =>
        this.restService.request<any, PagedResultDto<ProductDto>>({
            method: 'GET',
            url: '/api/app/product',
            params: {
                sorting: input.sorting,
                skipCount: input.skipCount,
                maxResultCount: input.maxResultCount,
                filter: input.filter
            },
        });

    update = (id: string, input: CreateUpdateProductDto) =>
        this.restService.request<any, ProductDto>({
            method: 'PUT',
            url: `/api/app/product/${id}`,
            body: input,
        });
}
