from django.db import models
from django.contrib.auth.models import User


class Patient(models.Model):
    national_code = models.CharField(max_length=10, unique=True)
    first_name = models.CharField(max_length=50)
    last_name = models.CharField(max_length=50)
    GENDER_CHOICES = [('M', 'مرد'), ('F', 'زن')]
    gender = models.CharField(max_length=1, choices=GENDER_CHOICES)
    mobile = models.CharField(max_length=11)
    address = models.TextField()
    user = models.ForeignKey(User, on_delete=models.SET_NULL, null=True)

    def __str__(self):
        return self.person.__str__()

class PatientService(models.Model):
    patient = models.ForeignKey(Patient, on_delete=models.CASCADE)
    user = models.ForeignKey(User, on_delete=models.SET_NULL, null=True)
    visit_date = models.DateField()
    wound_type = models.CharField(max_length=100)
    wound_location = models.CharField(max_length=100)
    session_number = models.PositiveIntegerField()
    description = models.TextField(blank=True)

    def __str__(self):
        return f"Service for {self.patient} on {self.visit_date}"