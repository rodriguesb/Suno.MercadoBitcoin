# Mercado Bitcoin Integration - .NET

Projeto de integração com a API do **Mercado Bitcoin** utilizando **C# .NET**, com foco em boas práticas de arquitetura, resiliência e testes automatizados.

---

## 📌 Objetivo

Criar uma biblioteca de integração capaz de:

- Consumir endpoints HTTP autenticados
- Realizar chamadas seguras e resilientes
- Simular falhas de API
- Garantir qualidade com testes automatizados
- Aplicar princípios de Clean Architecture e SOLID
- Demonstrar capacidade técnica em integrações reais

---

## 🏗 Estrutura do Projeto

```
src/
├── Suno.MercadoBitcoin.Domain
├── Suno.MercadoBitcoin.Application
├── Suno.MercadoBitcoin.Infra.External
└── Suno.MercadoBitcoin.Api

tests/
├── Suno.MercadoBitcoin.UnitTest
└── Suno.MercadoBitcoin.IntegrationTest
```

### 📁 Organização de Pastas

- `src/` → Código-fonte da aplicação  
- `tests/` → Testes automatizados

---

## 🧠 Organização em Camadas

### 📦 Domain
Contém:
- Regras de negócio
- Entidades
- Value Objects

---

### 🧩 Application
Responsável por:
- Casos de uso
- Interfaces de serviço
- DTOs
- Orquestração da lógica de negócio

---

### 🌐 Infra.External
Camada de integração externa.

Responsável por:
- Comunicação HTTP
- Autenticação via token
- Tratamento de falhas
- Retry automático

Tecnologias utilizadas:
- Refit
- Polly

---

### 🧪 Tests

#### ✅ Testes Unitários
- Validam regras de negócio
- Testam serviços isoladamente

#### ✅ Testes de Integração
- Simulam a API com WireMock
- Validam envio de headers
- Testam retry automático
- Forçam falhas HTTP 500
- Validam comportamento em timeout
- Simulam recuperação após falha

---

## 🌐 API Integrada

### 📍 Endpoint

```
GET /accounts/{accountId}/positions
```

---

### 🔐 Autenticação

A API utiliza **Bearer Token**.

Exemplo de header:

```
Authorization: Bearer {TOKEN}
```

---

## 🚀 Tecnologias

- .NET 8
- Refit
- Polly
- WireMock.Net
- FluentAssertions
- xUnit
- Injeção de Dependência

---

## 🔁 Resiliência com Polly

Implementado:

- Retry exponencial
- Policy de timeout
- Tratamento global de erros HTTP
- Repetição automática em falhas transitórias

---

## ⚙️ Configuração

Exemplo de registro do cliente HTTP:

```csharp
services.AddMercadoBitcoin("https://api.mercadobitcoin.net", TimeSpan.FromSeconds(10));
```

---

## ▶️ Executando os testes

```bash
dotnet test
```

---

## 👤 Autor

Projeto desenvolvido como teste técnico e demonstração de domínio em integração de APIs, arquitetura de software e automação de testes.

---
