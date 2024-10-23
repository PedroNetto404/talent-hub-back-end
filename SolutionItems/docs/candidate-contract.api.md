
# Candidate Api Contract

## Create Candidate

### Request
#### Uri:
- POST {{host}}/api/candidates
#### Body
```JSON
{
    "name" : "Pedro Netto de Sousa Lima",
    "summary": "Jovem desenvolvedor backend",
    "instagram_url": "https://instagram.com/nettoaoquadrado",
    "linkedin_url": "https://linkedin.com/nettopedro",
    "birth_date": "2000/08/29",
    "email": "pedronetto31415@gmail.com",
    "phone": "16981477900",
    "expected_remuneration": 5000.00,
    "address": {
        "street": 1327,
        
    }
}
```