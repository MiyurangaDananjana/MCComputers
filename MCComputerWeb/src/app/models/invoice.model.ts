export interface InvoiceItem {
    productId: number;
    quantity: number;
    price: number;
}

export interface Invoice {
    invoiceId?: number;
    customerId: number;
    totalAmmount: number;
    invoiceItems: InvoiceItem[];
}
