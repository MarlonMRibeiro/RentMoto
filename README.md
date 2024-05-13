# RentMoto

Este projeto é uma API que oferece funcionalidades essenciais para alugar motos e gerenciar a entrega de pedidos. Abaixo estão listadas as diferentes funcionalidades oferecidas, desde a criação de usuários até a finalização de locações de motocicletas.

## Índice

- [SOBRE](#sobre)
- [INSTALAÇÃO](#instalação)
- [FUNCIONALIDADES](#funcionalidades)

## SOBRE

Esta API foi desenvolvida para facilitar o processo de aluguel de motos e gestão de entregas. Oferece endpoints para criar usuários, autenticar, gerenciar entregadores, motocicletas, pedidos, notificações de pedidos e planos de aluguel.

## INSTALAÇÃO

Para executar este projeto localmente, siga estas etapas:

1. Clone o projeto para o seu ambiente local.
2. Abra o terminal na pasta do projeto clonado.
3. Execute o comando `docker-compose up`.

**Ao executar o `docker-compose up`, você iniciará os contêineres necessários para executar a API. Note que pode ocorrer uma mensagem indicando que a API está caindo, mas isso é esperado, pois o serviço do RabbitMQ ainda não foi iniciado.**


## FUNCIONALIDADES

## Usuário

- ### Criar usuário 

#### **POST** /CreateUser

Body:
```
{
  "userName": "string",
  "password": "string",
  "email": "user@example.com",
  "name": "string",
  "admin": true
}
```

Retorno:

```
{
  "success": true,
  "message": "Sucess to create user!"
}

```
- ### Autenticar (Login)
#### **POST** /Authenticate

Parâmetros:

```
Username: "string"
Password: "string" 
```

Retorno

```
{
  "id": "Guid",
  "name": "string",
  "login": "string",
  "token": "*token*"
}

```
### Autenticação Bearer
O esquema de autenticação Bearer funciona enviando um token de acesso no cabeçalho "Authorization" da solicitação HTTP. O token deve ser precedido pela palavra-chave "Bearer". Por exemplo:

```
Authorization: Bearer <token>
```
Onde <token> é o token de acesso fornecido para autenticar a solicitação.

## DeliveryMan

- ### Criar entregador

#### **POST** /DeliveryMan/CreateDeliveryMan

Parâmetros:
```
name: "string"
identity: "string"
cnpj: "string"
birth: DateTime (Exemplo: 2024-04-29T05:11:06.719Z)
cnhNumber: "string"
cnhType: "string"
```
cnhType:
  - 0 -> Tipo A
  - 1 -> Tipo B
  - 2 -> Tipo A e B
    
Body:
```
{
  "file": FILE,
}
```


Retorno:

```
{
  "success": true,
  "message": "Sucess to create delivery man!"
}

```
- ### Atualizar foto da cnh
#### **PUT** /DeliveryMan/UpdateCnhFile

Parâmetros:

```
file: FILE,
```

Retorno

Body:
```
{
  "success": true,
  "message": "Sucess to update file!"
}

```

## Motorcycle

- ### Criar moto

#### **POST** /Motorcycle/CreateMotorcycle

Body:
```
{
  "model": "string",
  "identity": "string",
  "licensePlate": "string",
  "year": "string"
}

```

Retorno:
```
{
  "success": true,
  "message": "Sucess to create motorcycle!"
}
```

- ### Pegar todas as motos
#### **GET** /Motorcycle/Get

Parâmetros:

```
Sem parâmetros
```

Retorno


Body:
```
[
    {
        "model": "string",
        "identity": "string",
        "licensePlate": "string",
        "year": "string",
        "status": int,
        "id": "Guid",
        "createdAt": "2024-04-28T22:02:09.925759Z",
        "deleteAt": "2024-04-28T22:02:09.925759Z"
    }
]
```


- ### Pegar moto por Id
#### **GET** /Motorcycle/GetMotorcycleByPlate?plate={plate}

Parâmetros:

```
Plate: string
```

Retorno


Body:
```
{
  "model": "string",
  "identity": "string",
  "licensePlate": "string",
  "year": "string",
  "status": int,
  "id": "Guid",
  "createdAt": "2024-04-28T22:02:09.925759Z",
  "deleteAt": "2024-04-28T22:02:09.925759Z"
}

```

- ### Deletar moto pela placa
#### **DELETE** /Motorcycle/Delete?plate={plate}

Parâmetros:

```
Plate: string
```

Retorno


Body:
```
{
  "success": true,
  "message": "Sucess to delete motorcycle!"
}

```

- ### Atualizar placa
#### **PUT** /Motorcycle/UpdateMotorcycle?plate={plate}

Parâmetros:

```
NewPlate: string
OldPlate: string
```

Retorno:

Body:
```
{
  "success": true,
  "message": "Sucess to delete motorcycle!"
}

```


## Order

- ### Criar pedido

#### **POST** /Order/CreateOrder

Body:
```
{
  "code": "string",
  "price": 0
}

```

Retorno:
```
{
  "success": true,
  "message": "Sucess to create order!"
}
```

- ### Iniciar um pedido
#### **POST** /Order/TakeOrder

Parâmetros:

```
OrderId: Guid
```

Retorno:
```
{
  "success": true,
  "message": "Sucess to take order!"
}
```


- ### Finalizar um pedido
#### **POST** /Order/FinalizeOrder

Parâmetros:

```
OrderId
```

Retorno:
```
{
  "success": true,
  "message": "Sucess to finalize order!"
}

```


## OrderNotification

- ### Pegar todas as notificações
#### **GET** /OrderNotification/Get

Parâmetros:

```
Sem parâmetros
```

Retorno:
```
[
  {
    "orderId": Guid
    "deliveryManId": Guid
    "message": "string",
    "id": Guid,
    "createdAt": "2024-04-28T22:25:22.649724Z",
    "deleteAt": "2024-04-28T22:25:22.649724Z"
  }
]

```


## Rental

- ### Pegar todos os planos
#### **GET** /Rental/GetPlan

Parâmetros:

```
Sem parâmetros
```

Retorno:
```
[
    {
        "days": 30,
        "dailyPrice": 22.0,
        "penaltyPercentage": 0.6,
        "id": Guid,
        "createdAt": "2024-04-28T20:10:24.034767Z",
        "deleteAt": "2024-04-28T20:10:24.034767Z"
    },
    {
        "days": 7,
        "dailyPrice": 30.0,
        "penaltyPercentage": 0.2,
        "id": Guid,
        "createdAt": "2024-04-28T20:10:24.034766Z",
        "deleteAt": "2024-04-28T20:10:24.034766Z"
    },
    {
        "days": 15,
        "dailyPrice": 28.0,
        "penaltyPercentage": 0.4,
        "id": Guid,
        "createdAt": "2024-04-28T20:10:24.034766Z",
        "deleteAt": "2024-04-28T20:10:24.034766Z"
    }
]

```

- ### Alugar uma moto
#### **POST** /Rental/RentMotorcycle

Parâmetros:

```
PlanId: Guid
```

Retorno:
```
{
  "success": true,
  "message": "Sucess to rent a motorcycle!"
}

```

- ### Consultar valor locação
#### **GET** /Rental/ConsultRentalValue

Parâmetros:

```
rentalId: Guid
endDate: DateTime (Exemplo: 2024-04-29T05:11:06.719Z)
```

Retorno:
```
{
  "success": true,
  "message": "Sucess to consult: Total Value R$ 0.00 "
}

```

- ### Finalizar locação
#### **GET** /Rental/ReturnMotorcycle

Parâmetros:

```
motorcycleId: Guid
```

Retorno:
```
{
  "success": true,
  "message": "Sucess to consult: Total Value R$ 0.00 "
}
```

