import { AuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface ProductDto extends AuditedEntityDto<string> {
  name: string;
  price: number;
  description?: string;
}

export interface CreateUpdateProductDto {
  name: string;
  price: number;
  description?: string;
}

export interface GetProductsInput extends PagedAndSortedResultRequestDto {
  filter?: string;
}
