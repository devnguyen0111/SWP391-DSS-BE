# **DIAMOND SHOP SYSTEM** 
#(Phần mềm quản lý cửa hàng kim cương)
## Functional requirements

_Phần mềm quản lý việc bán kim cương trực tuyến của công ty kinh doanh kim cương_
 - Trang chủ giới thiệu cửa hàng, sản phẩm về kim cương, bộ sưu tập, bảng giá kim cương, kiến thức trang sức, kiến thức kim cương, hướng dẫn chọn ni, câu hỏi thường gặp, …
 - Quản lý quá trình mua hàng của khách hàng.
   **Khách hàng chọn sản phẩm và đặt hàng --> NV bán hàng tiếp nhận đơn hàng và hướng dẫn đo ni cho khách hàng --> Khách hàng xác nhận ni và thực hiện thanh toán --> NV bán hàng xuất sản phẩm và kèm theo phiếu bảo hành và giấy chứng nhận kim cương để bàn giao --> NV giao hàng gửi sản phẩm đến khách hàng**
 - Quản lý phiếu bảo hành sản phẩm, giấy chứng nhận kim cương theo tiêu chuẩn 4C của GIA.
 - Quản lý chương trình khuyến mãi, tích lũy điểm.
 - Khai báo bảng giá viên kim cương theo các tiêu chí: loại nguồn gốc (diamond origin), trọng lượng (Carat weight), màu sắc (Color), độ tinh khiết (Clarity), cắt mài (Cut); khai báo bảng giá vỏ kim cương.
 - Quản lý sản phẩm kim cương bao gồm: vỏ kim cương, viên kim cương chính, các viên kim cương phụ, ...
   **Giá bán = giá vốn sản phẩm * tỉ lệ áp giá, Giá vốn sản phẩm = tiền kim cương + vỏ kim cương + tiền công**
 - Dashboard thống kê.

# Short Link
- [Github FE](https://github.com/devnguyen0111/SWP391-DiamondShopSystem)

# Testing
<details>
<summary>Use Case Description</summary>

| ID     | Use Case                                 | Actors       | Use Case Description                                                   |
| ------ | ---------------------------------------- | ------------ | ---------------------------------------------------------------------- |
| `UC-01`  | Browse diamond product                   | Guest        | Guests can browse available diamond products without logging in.       |
| `UC-02`  | View detailed product description        | Guest        | Guests can see detailed information about a specific product.          |
| `UC-03` | Register an account                      | Guest        | The system enables guests to create a new user account.                |
| `UC-04`  | Logout                                   | Customer     | Customers can log out of the system.                                   |
| `UC-05`  | Login                                    | Customer     | Customers can log in to the system.                                    |
| `UC-06`  | View Homepage                            | Customer     | Customers can access the homepage.                                     |
| `UC-07`  | Browse diamond product                   | Customer     | Customers can browse available diamond products.                       |
| `UC-08`  | View detailed product description        | Customer     | Customers can see detailed information about a specific product.       |
| `UC-09`  | View collections                         | Customer     | Customers can view different collections of diamonds and jewelry.      |
| `UC-10`  | View Feedback                            | Customer     | Customers can check feedback from other users.                         |
| `UC-11`  | View educational resources               | Customer     | Customers can access educational materials related to diamonds.        |
| `UC-12`  | View FAQs                                | Customer     | Customers can read frequently asked questions.                         |
| `UC-13`  | View store location                      | Customer     | Customers can find the physical store locations.                       |
| `UC-14`  | View designer list                       | Customer     | Customers can see the list of designers.                               |
| `UC-15`  | Update profile                           | Customer     | Customers can update their user information.                           |
| `UC-16`  | Manage profile                           | Customer     | Customers can manage their profile details.                            |
| `UC-17`  | Cancel order                             | Customer     | Customers can cancel an existing order.                                |
| `UC-18`  | Update order                             | Customer     | Customers can modify an existing order.                                |
| `UC-19`  | Add to cart                              | Customer     | Customers can add a product to the shopping cart.                      |
| `UC-20`  | Update cart                              | Customer     | Customers can update items in the shopping cart.                       |
| `UC-21`  | Delete cart                              | Customer     | Customers can remove items from the shopping cart.                     |
| `UC-22`  | View cart                                | Customer     | Customers can view items in the shopping cart.                         |
| `UC-23`  | Add to wishlist                          | Customer     | Customers can add a product to the wishlist.                           |
| `UC-24`  | Update wishlist                          | Customer     | Customers can modify items in the wishlist.                            |
| `UC-25`  | Remove wishlist item                     | Customer     | Customers can remove items from the wishlist.                          |
| `UC-26`  | View wishlist                            | Customer     | Customers can view items in the wishlist.                              |
| `UC-27`  | Confirm order's item info                | Customer     | Customers can confirm information of items before placing an order.    |
| `UC-28`  | Receive vouchers                         | Customer     | Customers can receive discount vouchers.                               |
| `UC-29`  | Proceed to payment                       | Customer     | Customers can make a payment for an order.                             |
| `UC-30`  | Track order status and shipment updates  | Customer     | Customers can track the status of an order and shipment updates.       |
| `UC-31`  | View orders history                      | Customer     | Customers can view the history of past orders.                         |
| `UC-32`  | View recently visited product            | Customer     | Customers can see a list of recently viewed products.                  |
| `UC-33`  | Review                                   | Customer     | Customers can submit a review for a product.                           |
| `UC-34`  | Choose shipping method                   | Customer     | Customers can select a shipping method for an order.                   |
| `UC-35`  | Choose payment method                    | Customer     | Customers can select a payment method for an order.                    |
| `UC-36`  | Assist customer                          | Sales Staff  | Sales staff can assist customers with their purchases.                 |
| `UC-37`  | View customer details                    | Sales Staff  | Sales staff can view detailed information of a customer.               |
| `UC-38`  | View assigned deliveries                 | Delivery Staff| Delivery staff can check deliveries assigned to them.                 |
| `UC-39`  | Update delivery status                   | Delivery Staff| Delivery staff can update the status of deliveries.                   |
| `UC-40`  | Send delivery status                     | Delivery Staff| Delivery staff can send status updates to customers.                  |
| `UC-41`  | View all customers                       | Manager      | Managers can view the list of all customers.                           |
| `UC-42`  | View all staff                           | Manager      | Managers can view the list of all staff members.                       |
| `UC-43`  | Manage staff account                     | Manager      | Managers can manage staff accounts and details.                        |
| `UC-44`  | View price                               | Manager      | Managers can check the price of products.                              |
| `UC-45`  | Set a price                              | Manager      | Managers can set the price for products.                               |
| `UC-46`  | Update price                             | Manager      | Managers can update the price of products.                             |
| `UC-47`  | Create a product                         | Manager      | Managers can create a new product in the system.                       |
| `UC-48`  | Manage a product                         | Manager      | Managers can manage existing products.                                 |
| `UC-49`  | Disable a product                        | Manager      | Managers can disable a product from being available.                   |
| `UC-50`  | Update information of a product          | Manager      | Managers can update details of a product.                              |
| `UC-51`  | Assign order                             | Manager      | Managers can assign orders to delivery staff.                          |
| `UC-52`  | Reject order                             | Manager      | Managers can reject a customer order.                                  |
| `UC-53`  | Approve order                            | Manager      | Managers can approve a customer order.                                 |
| `UC-54`  | Set discount rate                        | Manager      | Managers can set discount rates for products.                          |
| `UC-55`  | Manage order                             | Manager      | Managers can manage customer orders.                                   |
| `UC-56`  | View all orders                          | Manager      | Managers can view the list of all customer orders.                     |
| `UC-57`  | Upgrade customer loyalty level           | Manager      | Managers can upgrade the loyalty level of a customer.                  |
| `UC-58`  | View audit schedule                      | Manager      | Managers can view the schedule of system audits.                       |
| `UC-59`  | Print invoice                            | Admin        | Admins can generate and print an invoice for an order.                 |
| `UC-60`  | Print warranty card                      | Admin        | Admins can generate and print a warranty card.                         |
| `UC-61`  | Prepare custom invoice                   | Admin        | Admins can prepare a customized invoice.                               |
| `UC-62`  | Handle returns and exchanges             | Admin        | Admins can manage the process of returns and exchanges.                |
| `UC-63` | Manage customer review                   | Admin        | Admins can manage reviews submitted by customers.                      |
| `UC-64`  | Manage customer                          | Admin        | Admins can manage customer accounts and details.                       |
| `UC-65` | Disable a customer                       | Admin        | Admins can disable a customer account.                                 |
| `UC-66` | View a customer detail                   | Admin        | Admins can view detailed information of a customer.                    |

</details>
 
| Sprint | UC |
| --- | --- |
| Sprint 1 | `UC-02` `UC-03` `UC-05` `UC-07` `UC-15` `UC-28` `UC-29` `UC-30` `UC-35` `UC-44` `UC-45` `UC-46` `UC-47` `UC-60`  |

| Member | assigned UC |
| --- | --- |
| Huỳnh Minh Long |  |
| Nguyễn Cao Trí |  |
| Nguyễn Trần Hồng Phúc |  `UC-28`, |
| Lê Quang Vinh |  |
| Trần Hoàng Tuấn |  |


_FIN_
