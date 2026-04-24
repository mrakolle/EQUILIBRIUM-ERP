# EquillibriumERP

A multi-tenant ERP system built with .NET 10, PostgreSQL, and EF Core using schema-per-tenant isolation.

---

## 🧠 Architecture Overview

- Public schema stores tenant registry
- Each tenant gets isolated schema: tenant_{guid}
- Fully separated ERP data per tenant

---

## 🚀 Status

✔ Tenant registration working  
✔ Schema creation working  
✔ Migration system working  
✔ GitHub versioned (v1.0-multitenant-core)

---

## 📡 API

POST /api/tenants/create

{
  "name": "ACME Manufacturing"
}

---

## 🔐 Tenant Header

X-Tenant-Id: {tenant-guid}
