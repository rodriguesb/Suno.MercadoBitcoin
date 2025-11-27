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

src/
├── Suno.MercadoBitcoin.Domain
├── Suno.MercadoBitcoin.Application
├── Suno.MercadoBitcoin.Infra.External
└── Suno.MercadoBitcoin.Api

tests/
├── Suno.MercadoBitcoin.UnitTest
└── Suno.MercadoBitcoin.IntegrationTest

### 📁 Organização de Pastas

src/ → Código-fonte da aplicação
tests/ → Testes automatizados

---

## 🧠 Organização em Camadas

### Domain
Contém:
- Regras de negócio
- Entidades
- Value Objects

---

### Application
Responsável por:
- Casos de uso
- Interfaces de serviço
- DTOs
- Orquestração da lógica

---

### Infra.External
Camada de integração externa:

Responsável por:
- Comunicação HTTP
- Autenticação via token
- Tratamento de falhas
- Retry automático

Tecnologias:
- Refit
- Polly

---

### Tests
Inclui:

#### ✅ Testes Unitários
- Validam regras de negócio
- Testam serviços isoladamente

#### ✅ Testes de Integração
- Simulam API com WireMock
- Validam headers
- Testam retry
- Forçam falhas 500
- Verificam timeout
- Simulam sucesso após falha

---

## 🌐 API Integrada

### Endpoint:

GET /accounts/{accountId}/positions


### Autenticação:

Bearer Token:

Authorization: Bearer {TOKEN}


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
- Policy de Timeout
- Tratamento global de erro HTTP
- Repetição automática em falhas transitórias

---

## ⚙️ Configuração

Exemplo de注册ção do cliente HTTP:

```csharp
services.AddMercadoBitcoin("https://api.mercadobitcoin.net", TimeSpan.FromSeconds(10));
