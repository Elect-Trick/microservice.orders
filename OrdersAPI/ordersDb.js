//Defines mongo db
db = db.getSiblingDB('OrdersDB');
var orders = [{
        "id": "2c9972bc-0f10-4976-b119-fdd425bba4b8",
        "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "orderDate": "2026-07-13T10:49:31.798Z",
        "totalBill": 1029.98,
        "orderItems": [
            {
                "id": "b30f8abe-9e19-4828-a7da-ca7d82d454fc",
                "productId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
                "productName": "Laptop Tester",
                "unitPrice": 999.99,
                "quantity": 1,
                "totalPrice": 999.99
            },
            {
                "id": "dca8cc7c-dde0-4414-8608-3af5cfd00762",
                "productId": "b2c3d4e5-f6a7-8b9c-0d1e-2f3a4b5c6d7e",
                "productName": "Wireless Mouse",
                "unitPrice": 29.99,
                "quantity": 1,
                "totalPrice": 29.99
            }
        ]
}];
db.orders.insertMany(orders);
