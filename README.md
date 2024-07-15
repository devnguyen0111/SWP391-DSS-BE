# **DIAMOND SHOP SYSTEM** (Phần mềm quản lý cửa hàng kim cương)

## Functional requirements

_Phần mềm quản lý việc bán kim cương trực tuyến của công ty kinh doanh kim cương_

- Trang chủ giới thiệu cửa hàng, sản phẩm về kim cương, bộ sưu tập, bảng giá kim cương, kiến thức trang sức, kiến thức kim cương, hướng dẫn chọn ni, câu hỏi thường gặp, …
- Quản lý quá trình mua hàng của khách hàng.
  **Khách hàng chọn sản phẩm và đặt hàng --> NV bán hàng tiếp nhận đơn hàng và hướng dẫn đo ni cho khách hàng --> Khách hàng xác nhận ni và thực hiện thanh toán --> NV bán hàng xuất sản phẩm và kèm theo phiếu bảo hành và giấy chứng nhận kim cương để bàn giao --> NV giao hàng gửi sản phẩm đến khách hàng**
- Quản lý phiếu bảo hành sản phẩm, giấy chứng nhận kim cương theo tiêu chuẩn 4C của GIA.
- Quản lý chương trình khuyến mãi, tích lũy điểm.
- Khai báo bảng giá viên kim cương theo các tiêu chí: loại nguồn gốc (diamond origin), trọng lượng (Carat weight), màu sắc (Color), độ tinh khiết (Clarity), cắt mài (Cut); khai báo bảng giá vỏ kim cương.
- Quản lý sản phẩm kim cương bao gồm: vỏ kim cương, viên kim cương chính, các viên kim cương phụ, ...
  **Giá bán = giá vốn sản phẩm \* tỉ lệ áp giá, Giá vốn sản phẩm = tiền kim cương + vỏ kim cương + tiền công**
- Dashboard thống kê.

# Short Link

- [Github BackEnd](https://github.com/devnguyen0111/SWP391-DSS-BE)
- [Github FrontEnd](https://github.com/devnguyen0111/SWP391-DiamondShopSystem)
- [SWP391-NET1817](https://docs.google.com/spreadsheets/d/1kO166hgUD31-DIYq5_fsoDtkX_Ifbo-Fclt_xM124Bg/edit?gid=0#gid=0)
- [SWP391-Topic](https://docs.google.com/spreadsheets/d/1kO166hgUD31-DIYq5_fsoDtkX_Ifbo-Fclt_xM124Bg/edit?gid=2063864594#gid=2063864594)

# Screen

## Home

https://github.com/user-attachments/assets/f42f4514-dcda-4058-b977-0d2c8276b749

## Login

https://github.com/user-attachments/assets/8e1c945b-b293-41c1-9b46-d50939162222

## Product Menu
### Pendants
![pen-menu](https://github.com/user-attachments/assets/cc84050b-20f7-4307-888e-b9d113fbe715)
### Rings
![ring-menu](https://github.com/user-attachments/assets/dc6db33f-c509-4641-8ec3-49019a61ed33)
### Earrings
![ear-menu](https://github.com/user-attachments/assets/3f1ed7d1-bf0c-4880-a8cb-d54945e2e89e)
### Diamonds
![dia-menu](https://github.com/user-attachments/assets/e10f9490-d53c-4ef4-b103-737a47a01f92)
### Necklaces
![neck-menu](https://github.com/user-attachments/assets/acaed5c2-5d74-4a3e-9c90-9c28724624fa)

# Testing
<details>
<summary>Use Case Description</summary>
 
| ID     | Use Case                                   | Actors               | Use Case Description                                                                 |
|--------|--------------------------------------------|----------------------|--------------------------------------------------------------------------------------|
| UC-01  | Register an account                        | Guest                | The system enables guests to create a new user account.                              |
| UC-02  | Login                                      | Registered Users     | Registered Users can log in to the system.                                           |
| UC-03  | Logout                                     | Registered Users     | Registered Users can log out of the system.                                          |
| UC-04  | Forget password                            | Registered Users     | Registered Users can reset password if they forget their current password.           |
| UC-05  | View Homepage                              | Customer             | Customers can access the homepage.                                                   |
| UC-06  | Browse diamond product                     | Customer             | Customers can browse available diamond products.                                     |
| UC-07  | View detailed product description          | Customer             | Customers can see detailed information about a specific product.                     |
| UC-09  | View Feedback                              | Customer             | Customers can check feedback from other users.                                       |
| UC-10  | View educational resources                 | Customer             | Customers can access educational materials related to diamonds.                      |
| UC-11  | View FAQs                                  | Customer             | Customers can read frequently asked questions.                                       |
| UC-12  | Manage profile                             | Customer             | Customers can view and update their profile details.                                 |
| UC-13  | View cart                                  | Customer             | Customers can view items in the shopping cart.                                       |
| UC-14  | Add to cart                                | Customer             | Customers can add a product to the shopping cart.                                    |
| UC-15  | Update cart                                | Customer             | Customers can update items in the shopping cart.                                     |
| UC-16  | Delete cart                                | Customer             | Customers can remove items from the shopping cart.                                   |
| UC-17  | Create Order                               | Customer             | Customers can create an order based on their products (inside cart or instantly buy) |
| UC-18  | Update order                               | Customer             | Customers can modify an existing order.                                              |
| UC-19  | Cancel order                               | Customer             | Customers can cancel an existing order.                                              |
| UC-20  | View wishlist                              | Customer             | Customers can view items in the wishlist.                                            |
| UC-21  | Add to wishlist                            | Customer             | Customers can add a product to the wishlist.                                         |
| UC-22  | Update wishlist                            | Customer             | Customers can modify items in the wishlist.                                          |
| UC-23  | Remove wishlist item                       | Customer             | Customers can remove items from the wishlist.                                        |
| UC-24  | Confirm order's item info                  | Customer             | Customers can confirm information of items before placing an order.                  |
| UC-25  | Receive vouchers                           | Customer             | Customers can receive discount vouchers.                                             |
| UC-26  | Proceed to payment                         | Customer             | Customers can make a payment for an order.                                           |
| UC-27  | Track order status and shipment updates    | Customer             | Customers can track the status of an order and shipment updates.                     |
| UC-28  | View orders history                        | Customer             | Customers can view the history of past orders.                                       |
| UC-30  | Review Product                             | Customer             | Customers can submit a review for a product.                                         |
| UC-31  | Choose payment method                      | Customer             | Customers can select a payment method for an order.                                  |
| UC-32  | Choose shipping method                     | Customer             | Customers can select a shipping method for an order.                                 |
| UC-33  | Assist customer                            | Sales Staff          | Sales staff can assist customers with their purchases through email.                 |
| UC-34  | Assign order                               | Sales Staff / Manager| Sales staff / Managers can assign orders to specific delivery staff / Sale Staff.    |
| UC-35  | View list of assigned order                | Delivery Staff / Sale Staff | Sales staff/Delivery Staff can view a list of orders assigned by their Manager/ Sale Staff. |
| UC-36  | View customer's order details              | Delivery Staff / Sale Staff | Sales staff and Delivery Staff can view detailed information of a customer's order. |
| UC-37  | Request Cancel order permission            | Delivery Staff / Sale Staff | Sales staff and Delivery Staff can request permission to cancel an order from their Manager. |
| UC-38  | View assigned deliveries                   | Delivery Staff       | Delivery staff can check deliveries assigned to them.                                |
| UC-39  | Confirm delivered order                    | Delivery Staff       | Delivery staff can confirm the order that has already been delivered to customer; therefore, change the status of order. |
| UC-40  | View all sale staff                        | Manager              | Managers can view the list of all sale staff members to assign.                      |
| UC-41  | Send GIA                                   | Manager / Sale Staff | Managers and Sale Staff send the GIA for the diamond of the specific order.          |
| UC-42  | Create a product                           | Manager              | Managers can create a new product in the system.                                     |
| UC-43  | Update information of a product            | Manager              | Managers can update details of a product.                                            |
| UC-44  | Disable a product                          | Admin                | Admin can disable a product from being available.                                    |
| UC-45  | Cancel order                               | Manager              | Managers can cancel a customer's order.                                              |
| UC-46  | Approve/Reject Cancel order permission     | Manager              | Managers can approve or reject the request from sales and delivery staff.            |
| UC-47  | View created orders                        | Manager              | Managers can view the list of all customer orders that have just been created.       |
| UC-48  | Print warranty card                        | Manager / Sale Staff | Managers and Sale Staff can generate and print a warranty card.                      |
| UC-49  | Manage dashboard                           | Admin                | Admins can manage the dashboard of the system.                                       |
| UC-50  | Disable a user                             | Admin                | Admins can disable a customer account.                                               |
</details>
 
| Sprint | UC |
| --- | --- |
| Sprint 1 | `UC-01` `UC-02` `UC-03` `UC-04` `UC-05` `UC-06` `UC-07` `UC-10` `UC-11` `UC-12` |
| Sprint 2 | `UC-13` `UC-14` `UC-15` `UC-16` `UC-17` `UC-18` `UC-24` `UC-26` `UC-31` `UC-32` `UC-34` `UC-35` `UC-36` `UC-37` `UC-38` `UC-39` `UC-40` `UC47` |
| Sprint 3 | `UC-09` `UC-19` `UC-20` `UC-21` `UC-22` `UC-23` `UC-25` `UC-27` `UC-28` `UC-30` `UC-41` `UC-45` `UC-46` `UC-48` |
| Sprint 4 | `UC-33` `UC-42` `UC-43` `UC-44` `UC-49` `UC-50` |

| Member | Sprint 1 | Sprint 2 | Sprint 3 | Sprint 4 |
| --- | --- | --- | --- | --- |
| Huỳnh Minh Long |`UC-05` `UC-07` | `UC-34` `UC-35` `UC-36` `UC-39` `UC-40` | `UC-27` `UC-48` | `UC-49` |
| Nguyễn Cao Trí | `UC-04` `UC-06` | `UC-13` `UC-14` `UC-15` `UC-16` | `UC-25` `UC-41` | `UC-42` `UC-43` |
| Nguyễn Trần Hồng Phúc | `UC-01` `UC-02` `UC-03` | `UC-31` `UC-32` `UC-37` `UC-38` `UC-47` | `UC-30` | `UC-50` |
| Lê Quang Vinh | `UC-10` `UC-11` | `UC-17` `UC-24` `UC-26` | `UC-09` `UC-19` `UC-45` `UC-46` | `UC-44` |
| Trần Hoàng Tuấn | `UC-12` | `UC-18` | `UC-20` `UC-21` `UC-22` `UC-23` `UC-28` | `UC-33` |

_the end_
